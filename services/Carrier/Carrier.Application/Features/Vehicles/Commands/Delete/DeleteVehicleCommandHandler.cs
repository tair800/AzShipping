using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Commands.Delete;

public class DeleteVehicleCommandHandler(IVehicleRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteVehicleCommand, bool>
{
    public async Task<bool> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var num = existing.VehicleNumber;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Vehicle deleted", $"vehicle: {num} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
