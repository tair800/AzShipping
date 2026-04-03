using MrStyx.Domain.SeedWork.DomainEvents;

namespace Identity.Domain.AggregatesModel.RoleAggregate.Events;

public sealed class RoleCreatedDomainEvent : DomainEvent
{
    public string Name { get; }

    public RoleCreatedDomainEvent(string name)
    {
        Name = name;
    }
}