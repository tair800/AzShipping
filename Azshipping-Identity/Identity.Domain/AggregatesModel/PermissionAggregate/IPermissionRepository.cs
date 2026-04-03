using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Domain.AggregatesModel.PermissionAggregate;

public interface IPermissionRepository : IRepository<Permission, long>
{
    Task<bool> UserHasPermissionAsync(long userId, string permission);
    Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(long userId);
    Task<IReadOnlyCollection<string>> GetUserRolesAsync(long userId);
}