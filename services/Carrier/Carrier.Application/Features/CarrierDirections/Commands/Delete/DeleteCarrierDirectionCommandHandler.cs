using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Commands.Delete;

public class DeleteCarrierDirectionCommandHandler(ICarrierDirectionRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteCarrierDirectionCommand, bool>
{
    public async Task<bool> Handle(DeleteCarrierDirectionCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var carrierId = entity.CarrierId;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier direction deleted", $"carrier direction: carrier {carrierId} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
