using Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MrStyx.API.Common;
using MrStyx.Application.Exceptions.Constants;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Identity.API.Extensions;

public static class JwtAuthenticationExtension
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>().Bind(configuration.GetSection("JWT"));

        var jwt = configuration.GetSection("JWT").Get<JwtOptions>() ?? new JwtOptions();

        var secret = configuration["JWT:SecretKey"];
        if (string.IsNullOrWhiteSpace(secret)) throw new InvalidOperationException("JWT:SecretKey is required (use user-secrets/ENV/KeyVault).");

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,

                ValidateAudience = true,
                ValidAudience = jwt.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var payload = ApiResponse.Fail
                    (
                        message: "Unauthorized",
                        errors: new[] { "Authentication is required or token is invalid/expired." },
                        code: ErrorCodes.Unauthorized,
                        traceId: context.HttpContext.TraceIdentifier
                    );

                    await context.Response.WriteAsJsonAsync(payload, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });
                }
            };
        });

        return services;
    }
}
