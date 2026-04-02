using Clients.Application.Services;
using Clients.Domain.AggregatesModel.NegotiationAggregate;
using MediatR;

namespace Clients.Application.Features.Negotiations.Commands.Delete;

public sealed class DeleteNegotiationCommandHandler(INegotiationRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteNegotiationCommand, bool>
{
    public async Task<bool> Handle(DeleteNegotiationCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var clientId = entity.ClientId;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Client negotiation deleted", $"negotiation: client {clientId} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
