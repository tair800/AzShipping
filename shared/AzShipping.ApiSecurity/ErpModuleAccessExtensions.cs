using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzShipping.ApiSecurity;

public static class ErpModuleAccessExtensions
{
    public static IServiceCollection AddErpModuleAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ErpModuleAccessOptions>(configuration.GetSection(ErpModuleAccessOptions.SectionName));
        return services;
    }

    public static IApplicationBuilder UseErpModuleAccess(this IApplicationBuilder app) =>
        app.UseMiddleware<ErpModuleAccessMiddleware>();
}
