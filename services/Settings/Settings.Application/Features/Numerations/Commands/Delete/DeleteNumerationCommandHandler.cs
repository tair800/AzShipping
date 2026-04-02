using MediatR;
using Settings.Application.Services;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Application.Features.Numerations.Commands.Delete;

public sealed class DeleteNumerationCommandHandler(INumerationRepository repository, IInternalActionLogService actionLog)
    : IRequestHandler<DeleteNumerationCommand, bool>
{
    public async Task<bool> Handle(DeleteNumerationCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        var name = entity.Name;
        await repository.DeleteAsync(request.Id, ct);
        await actionLog.LogAsync("Numeration deleted", $"numeration: {name} • id: {request.Id}", null, null, ct);
        return true;
    }
}
