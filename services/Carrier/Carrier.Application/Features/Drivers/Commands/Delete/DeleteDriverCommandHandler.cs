using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.Delete;

public class DeleteDriverCommandHandler(IDriverRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteDriverCommand, bool>
{
    public async Task<bool> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var name = $"{entity.Name} {entity.Surname}";
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Driver deleted", $"driver: {name} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
