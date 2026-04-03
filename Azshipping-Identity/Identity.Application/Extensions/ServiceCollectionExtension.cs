using FluentValidation;
using Identity.Application.Extensions.MapsterExtensions;
using Identity.Application.Rules.RoleRules;
using Identity.Application.Rules.UserRules;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtension).Assembly));

        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly);

        services.AddPipelines();

        services.AddMapsterConfiguration();

        services.AddScoped<IRoleRules, RoleRules>();
        services.AddScoped<IUserRules, UserRules>();

        return services;
    }
}