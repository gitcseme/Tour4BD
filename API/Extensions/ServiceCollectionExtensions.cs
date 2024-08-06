using API.Helpers;

using Membership.Authentication;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        var migrationHelper = new MigrationHelper(app);
        await migrationHelper.SeedAsync();

    }

    public static IServiceCollection AddAppSettingsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfiguration>(configuration.GetSection(nameof(JwtConfiguration)));

        return services;
    }

    public static IServiceCollection AddAuthenticationWithJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection(nameof(JwtConfiguration)).Get<JwtConfiguration>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig?.Issuer,
                    //ValidAudience = jwtConfig.Audience
                    ClockSkew = TimeSpan.FromSeconds(0),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig?.SecretKey!))
                };
            });

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(cfg =>
        {
            cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "tourBD API", Version = "v1" });

            // Configure JWT Authentication in Swagger
            cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}
