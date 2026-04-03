using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.PermissionAggregate;

namespace Identity.Infrastructure.Services;

public class PermissionReadService(IPermissionRepository permissionRepository) : IPermissionReadService
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
     
    public Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(long userId) => _permissionRepository.GetUserPermissionsAsync(userId);

    public Task<IReadOnlyCollection<string>> GetUserRolesAsync(long userId) => _permissionRepository.GetUserRolesAsync(userId);

    public Task<bool> UserHasPermissionAsync(long userId, string permission) => _permissionRepository.UserHasPermissionAsync(userId, permission);
}