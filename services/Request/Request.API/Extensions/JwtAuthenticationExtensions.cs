using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Request.API.Options;

namespace Request.API.Extensions;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        var jwt = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();
        var secret = configuration["JWT:SecretKey"];
        if (string.IsNullOrWhiteSpace(secret))
            throw new InvalidOperationException("JWT:SecretKey is required.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30),
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        var log = ctx.HttpContext.RequestServices.GetService<ILoggerFactory>()?.CreateLogger("JwtAuth");
                        var hasAuth = ctx.Request.Headers.TryGetValue("Authorization", out var auth);
                        log?.LogWarning("[JWT] Auth failed. Has Authorization header: {HasAuth}. Error: {Error}",
                            hasAuth, ctx.Exception?.Message ?? "unknown");
                        return Task.CompletedTask;
                    },
                    OnChallenge = ctx =>
                    {
                        var log = ctx.HttpContext.RequestServices.GetService<ILoggerFactory>()?.CreateLogger("JwtAuth");
                        var hasAuth = ctx.Request.Headers.TryGetValue("Authorization", out var auth);
                        log?.LogWarning("[JWT] 401 Challenge. Has Authorization header: {HasAuth}. Error: {Error}",
                            hasAuth, ctx.AuthenticateFailure?.Message ?? "unauthorized");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}

