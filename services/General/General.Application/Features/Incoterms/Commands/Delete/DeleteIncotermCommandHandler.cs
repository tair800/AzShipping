using General.Application.Services;
using General.Domain.AggregatesModel.IncotermAggregate;
using MediatR;

namespace General.Application.Features.Incoterms.Commands.Delete;

public class DeleteIncotermCommandHandler(IIncotermRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteIncotermCommand, bool>
{
    public async Task<bool> Handle(DeleteIncotermCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;

        var data = $"{entity.Code} ({entity.Name})";
        var id = entity.Id;

        if (request.SoftDelete)
            await repository.SoftDeleteAsync(request.Id, cancellationToken);
        else
            await repository.DeleteAsync(request.Id, cancellationToken);

        await actionLogClient.LogAsync("Incoterm deleted", $"incoterm: {data} • id: {id}", null, null, cancellationToken);
        return true;
    }
}
