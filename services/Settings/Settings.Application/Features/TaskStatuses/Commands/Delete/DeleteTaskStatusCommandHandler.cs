using MediatR;
using Settings.Domain.AggregatesModel.TaskStatusAggregate;

namespace Settings.Application.Features.TaskStatuses.Commands.Delete;

public sealed class DeleteTaskStatusCommandHandler(ITaskStatusRepository repository) : IRequestHandler<DeleteTaskStatusCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
