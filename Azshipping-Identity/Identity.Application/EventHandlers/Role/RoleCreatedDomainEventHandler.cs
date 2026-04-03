using Identity.Domain.AggregatesModel.RoleAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using MrStyx.Application.EventHandlers;

namespace Identity.Application.EventHandlers.Role;

public sealed class RoleCreatedDomainEventHandler(ILogger<RoleCreatedDomainEventHandler> logger) : INotificationHandler<DomainEventNotification<RoleCreatedDomainEvent>>
{
    private readonly ILogger<RoleCreatedDomainEventHandler> _logger = logger;

    public Task Handle(DomainEventNotification<RoleCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        var ev = notification.DomainEvent;

        _logger.LogInformation("Role created. Name: {Name}", ev.Name);
        return Task.CompletedTask;
    }
}