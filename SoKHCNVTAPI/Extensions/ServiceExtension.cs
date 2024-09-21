using System;
using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Extensions;

public static class ServiceExtension
{
    public static void ConfigureServices(this IServiceCollection services, AppSettings settings)
    {
        // Cors
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                    builder
                        .WithOrigins(settings.AllowedHosts)
                        .WithMethods("GET", "POST", "PUT", "DELETE")
                        .AllowAnyOrigin()
                        .AllowAnyHeader());
        });

        // MySql
        // services.AddDbContext<RepositoryContext>(options => options.UseMySQL(settings.ConnectionStrings.MySql));

        // Postgres
        //services.AddDbContext<DataContext>(options => options.UseNpgsql(settings.ConnectionStrings.PostgresSql));
        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(settings.ConnectionStrings.PostgresSql);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddScoped<ITokenService, TokenService>();

        // Redis
        // services.AddStackExchangeRedisCache(options => { options.Configuration = settings.ConnectionStrings.Redis; });
        // services.AddTransient<RedisCacheService>();
        services.AddMemoryCache();

        // Auth
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.JwtKey.ValidIssuer,
                    ValidAudience = settings.JwtKey.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // Call this to skip the default logic and avoid using the default response
                        // Gọi hàm này để bỏ qua logic mặc định và tránh sử dụng phản hồi mặc định
                        context.HandleResponse();

                        // Write to the response in any way you wish
                        // Viết vào phản hồi bằng bất kỳ cách nào bạn muốn
                        context.Response.StatusCode = 401;
                        var response = new ExceptionBaseResponse
                        {
                            Message = "Unauthorized",
                            ErrorCode = (int)APIErrorCode.Unauthorized,
                            Success = false
                        };
                        await context.Response.WriteAsync(response.ToString());
                    }
                };
            });
        services.AddAuthorization();
        //
        // Api Version
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        })
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

        services.AddScoped<ApiKeyAuthFilter>(); //?[ApiKey]
        services.AddHttpContextAccessor();
    }
}