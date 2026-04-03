using Identity.Application.Interfaces.Services;
using Identity.Infrastructure.Options;
using Identity.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Extensions;

public static class RefreshTokenOptionsExtension
{
    public static IServiceCollection AddRefreshTokenOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<RefreshTokenOptions>()
                .Bind(configuration.GetSection("RefreshToken"))
                .Validate(o => o.LifeTimeDays >= 1 && o.LifeTimeDays <= 365, "LifeTimeDays must be 1..365")
                .ValidateOnStart();

        services.AddSingleton<ITokenService, TokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        return services;
    }
}