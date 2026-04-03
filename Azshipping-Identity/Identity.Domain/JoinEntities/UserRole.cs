using Identity.Domain.AggregatesModel.UserAggregate;

namespace Identity.Domain.JoinEntities;

public class UserRole
{
    public long UserId { get; private set; }
    public User User { get; private set; } = null!;
    public long RoleId { get; private set; }

    private UserRole() { }

    internal UserRole(long roleId) => RoleId = roleId;
}