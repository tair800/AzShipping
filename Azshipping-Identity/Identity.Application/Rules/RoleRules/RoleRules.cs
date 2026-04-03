using Identity.Domain.AggregatesModel.PermissionAggregate;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using MrStyx.Application.Exceptions;

namespace Identity.Application.Rules.RoleRules;

public class RoleRules(IRoleRepository roleRepository, IPermissionRepository permissionRepository, IUserRepository userRepository) : IRoleRules
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task FindMissingPermissionsAsync(IReadOnlyCollection<long> permissionIds, CancellationToken cancellationToken)
    {
        var ids = permissionIds.Distinct().ToList();
        var permissions = await _permissionRepository.GetByIdsAsync(ids, cancellationToken);
        var found = permissions.Select(p => p.Id).ToHashSet();

        var missing = ids.Where(id => !found.Contains(id)).ToList();

        if (missing.Any())
            throw new NotFoundException($"Can not find permissions by this ids: {string.Join(", ", missing)}");
    }

    public async Task IsAssignedToUserAsync(long roleId, CancellationToken cancellationToken)
    {
        var hasUsers = await _userRepository.GetSelectAsync(
            u => u.UserRoles.Any(ur => ur.RoleId == roleId),
            u => u.Username,
            cancellationToken
        );

        if (hasUsers.Count != 0)
        {
            var list = string.Join("\n", hasUsers);

            throw new ConflictException($"A role cannot be deleted while it is assigned to a users.\nUsers: {list}");
        }
    }

    public async Task RoleUniquenessCheckAsync(string name, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.GetFirstOrDefaultAsync(r => r.Name == name, cancellationToken);
        if (existingRole is not null) throw new ConflictException("Role already exists");
    }

    public async Task RoleUniquenessCheckAsync(string name, long roleId, CancellationToken cancellationToken)
    {
        var existingRole = await _roleRepository.GetFirstOrDefaultAsync(r => r.Name == name && r.Id != roleId, cancellationToken);
        if (existingRole is not null) throw new ConflictException("Role already exists");
    }
}