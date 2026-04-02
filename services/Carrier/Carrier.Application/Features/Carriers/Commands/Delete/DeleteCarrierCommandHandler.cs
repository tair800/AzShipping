using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.Carriers.Commands.Delete;

public sealed class DeleteCarrierCommandHandler(ICarrierRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteCarrierCommand, bool>
{
    public async Task<bool> Handle(DeleteCarrierCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var name = entity.Name;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier deleted", $"carrier: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
