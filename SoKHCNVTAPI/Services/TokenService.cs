using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;

namespace SoKHCNVTAPI.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken(User user);
    ClaimsPrincipal GetPrincipalFromToken(string token, bool isToken = true);
    bool ValidateToken(string token);
}

public class TokenService : ITokenService
{
    private readonly AppSettings _settings;

    public TokenService(IOptions<AppSettings> settings) { _settings = settings.Value; }

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Role, user.Role),
        };
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey.Secret));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _settings.JwtKey.ValidIssuer,
            Audience = _settings.JwtKey.ValidAudience,
            Expires = DateTime.Now.AddHours(24),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtKey.SecretRefresh));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _settings.JwtKey.ValidIssuer,
            Audience = _settings.JwtKey.ValidAudience,
            Expires = DateTime.Now.AddMonths(1),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token, bool isToken = true)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _settings.JwtKey.ValidIssuer,
            ValidAudience = _settings.JwtKey.ValidAudience,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(isToken ? _settings.JwtKey.Secret : _settings.JwtKey.SecretRefresh)),
            ClockSkew = TimeSpan.Zero
        };

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = (JwtSecurityToken)securityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
      

        return principal;
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_settings.JwtKey.Secret);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        _ = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = (JwtSecurityToken)securityToken;

        return jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase);
    }
}