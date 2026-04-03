using MrStyx.Domain.SeedWork.DomainEvents;

namespace Identity.Domain.AggregatesModel.UserAggregate.Events;

public sealed class UserCreatedDomainEvent : DomainEvent
{
    public string Email { get; }
    public string UserName { get; }

    public UserCreatedDomainEvent(string email, string userName)
    {
        Email = email;
        UserName = userName;
    }
}