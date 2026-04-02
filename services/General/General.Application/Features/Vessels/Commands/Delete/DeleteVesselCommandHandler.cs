using General.Application.Services;
using General.Domain.AggregatesModel.VesselAggregate;
using MediatR;

namespace General.Application.Features.Vessels.Commands.Delete;

public class DeleteVesselCommandHandler(IVesselRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteVesselCommand, bool>
{
    public async Task<bool> Handle(DeleteVesselCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;

        var name = entity.Name;
        var id = entity.Id;

        if (request.SoftDelete)
            await repository.SoftDeleteAsync(request.Id, cancellationToken);
        else
            await repository.DeleteAsync(request.Id, cancellationToken);

        await actionLogClient.LogAsync("Vessel deleted", $"vessel: {name} • id: {id}", null, null, cancellationToken);
        return true;
    }
}
