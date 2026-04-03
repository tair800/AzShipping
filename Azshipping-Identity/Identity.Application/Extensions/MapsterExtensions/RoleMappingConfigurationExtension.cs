using Identity.Application.DTOs.Role;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Extensions.MapsterExtensions;

public static class RoleMappingConfigurationExtension
{
    public static IServiceCollection AddRoleMappingConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Role, RoleDto>.NewConfig()
        .Map(dest => dest.PermissionIds, src => src.RolePermissions.Select(rp => rp.PermissionId).ToList());

        TypeAdapterConfig<IEnumerable<Role>, RoleList>.NewConfig()
        .Map(dest => dest.Roles, src => src.Adapt<IReadOnlyCollection<RoleDto>>());

        return services;
    }
}