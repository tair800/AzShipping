using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.AirlineAggregate;
using MediatR;

namespace Carrier.Application.Features.Airlines.Commands.Delete;

public class DeleteAirlineCommandHandler(IAirlineRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteAirlineCommand, bool>
{
    public async Task<bool> Handle(DeleteAirlineCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        var name = existing.Name;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Airline deleted", $"airline: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
