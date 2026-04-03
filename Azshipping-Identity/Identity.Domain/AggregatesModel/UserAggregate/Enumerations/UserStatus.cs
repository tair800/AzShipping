using MrStyx.Domain.SeedWork.Abstracts;

namespace Identity.Domain.AggregatesModel.UserAggregate.Enumerations;

public sealed class UserStatus : Enumeration
{
    private UserStatus(int id, string name) : base(id, name) { }

    public static readonly UserStatus Active = new(1, nameof(Active));
    public static readonly UserStatus Pending = new(2, nameof(Pending));
    public static readonly UserStatus Blocked = new(3, nameof(Blocked));
    public static readonly UserStatus Deactivated = new(4, nameof(Deactivated));
    public static readonly UserStatus Deleted = new(5, nameof(Deleted));

    public static IReadOnlyCollection<UserStatus> GetAll() => GetAll<UserStatus>();
}