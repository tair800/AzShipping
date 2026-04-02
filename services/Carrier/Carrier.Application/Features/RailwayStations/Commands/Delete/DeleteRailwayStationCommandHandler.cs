using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Commands.Delete;

public class DeleteRailwayStationCommandHandler(IRailwayStationRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteRailwayStationCommand, bool>
{
    public async Task<bool> Handle(DeleteRailwayStationCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var name = existing.Name;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Railway station deleted", $"railway station: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
