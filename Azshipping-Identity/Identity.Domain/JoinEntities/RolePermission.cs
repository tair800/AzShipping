using Identity.Domain.AggregatesModel.RoleAggregate;

namespace Identity.Domain.JoinEntities;

public class RolePermission
{
    public long RoleId { get; private set; }
    public Role Role { get; private set; } = null!;

    public long PermissionId { get; private set; }

    private RolePermission() { }

    internal RolePermission(long permissionId) => PermissionId = permissionId;
}