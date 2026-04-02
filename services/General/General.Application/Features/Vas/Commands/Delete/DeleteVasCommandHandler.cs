using General.Application.Services;
using General.Domain.AggregatesModel.VasAggregate;
using MediatR;

namespace General.Application.Features.Vas.Commands.Delete;

public class DeleteVasCommandHandler(IVasRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteVasCommand, bool>
{
    public async Task<bool> Handle(DeleteVasCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;

        var name = entity.Name;
        var id = entity.Id;

        if (request.SoftDelete)
            await repository.SoftDeleteAsync(request.Id, cancellationToken);
        else
            await repository.DeleteAsync(request.Id, cancellationToken);

        await actionLogClient.LogAsync("Vas deleted", $"vas: {name} • id: {id}", null, null, cancellationToken);
        return true;
    }
}
