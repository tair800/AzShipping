namespace Identity.Application.Rules.RoleRules;

public interface IRoleRules
{
    Task RoleUniquenessCheckAsync(string name, CancellationToken cancellationToken);
    Task RoleUniquenessCheckAsync(string name, long roleId, CancellationToken cancellationToken);
    Task FindMissingPermissionsAsync(IReadOnlyCollection<long> permissionIds, CancellationToken cancellationToken);
    Task IsAssignedToUserAsync(long roleId, CancellationToken cancellationToken);
}