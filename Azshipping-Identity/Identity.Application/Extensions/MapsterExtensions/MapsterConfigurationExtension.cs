using Identity.Application.DTOs.Permission;
using Identity.Domain.AggregatesModel.PermissionAggregate;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Extensions.MapsterExtensions;

public static class MapsterConfigurationExtension
{
    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
    {
        services.AddUserMappingConfiguration();

        services.AddRoleMappingConfiguration();

        TypeAdapterConfig<IEnumerable<Permission>, PermissionList>.NewConfig()
        .Map(dest => dest.Permissions, src => src.Adapt<IReadOnlyCollection<PermissionDto>>());

        return services;
    }
}