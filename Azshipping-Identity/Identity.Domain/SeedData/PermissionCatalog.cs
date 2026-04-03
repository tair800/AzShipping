using Azshipping.Auth;

namespace Identity.Domain.SeedData;

public static class PermissionCatalog
{
    public record PermissionInfo(string Module, string Name)
    {
        public string Code => $"{Module}.{Name}";
    }

    public static readonly PermissionInfo[] All =
    {
        new(Modules.Identity, PermissionOperations.UsersViewOp),
        new(Modules.Identity, PermissionOperations.UsersEditOp),
        new(Modules.Identity, PermissionOperations.RolesViewOp),
        new(Modules.Identity, PermissionOperations.RolesEditOp),
        new(Modules.Identity, PermissionOperations.PermissionsViewOp)
    };
}