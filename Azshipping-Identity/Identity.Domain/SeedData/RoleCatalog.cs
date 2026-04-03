using Azshipping.Auth;

namespace Identity.Domain.SeedData;

public static class RoleCatalog
{
    public record RoleInfo(string Name);

    public static readonly RoleInfo[] All =
    {
        new(Roles.Admin),
        new(Roles.Manager),
        new(Roles.HR),
        new(Roles.Accountant)
    };
}