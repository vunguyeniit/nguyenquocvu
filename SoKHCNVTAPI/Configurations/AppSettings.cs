namespace SoKHCNVTAPI.Configurations;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; init; } = null!;
    public JwtKey JwtKey { get; init; } = null!;
    public DefaultApiVersion DefaultApiVersion { get; init; } = null!;
    
    public string[] AllowedHosts { get; init; } = null!;
}

public class ConnectionStrings
{
    public string PostgresSql { get; set; } = null!;
}

public class JwtKey
{
    public string Secret { get; set; } = null!;
    public string SecretRefresh { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
}

public class DefaultApiVersion
{
    public short MajorVersion { get; set; }
    public short MinorVersion { get; set; }
}