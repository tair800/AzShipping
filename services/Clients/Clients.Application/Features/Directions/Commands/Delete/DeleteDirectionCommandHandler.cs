using Clients.Application.Services;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using MediatR;

namespace Clients.Application.Features.Directions.Commands.Delete;

public sealed class DeleteDirectionCommandHandler(IDirectionRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteDirectionCommand, bool>
{
    public async Task<bool> Handle(DeleteDirectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var clientId = entity.ClientId;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Client direction deleted", $"direction: client {clientId} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
