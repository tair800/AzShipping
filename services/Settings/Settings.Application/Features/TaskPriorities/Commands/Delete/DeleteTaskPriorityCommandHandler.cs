using MediatR;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;

namespace Settings.Application.Features.TaskPriorities.Commands.Delete;

public sealed class DeleteTaskPriorityCommandHandler(ITaskPriorityRepository repository) : IRequestHandler<DeleteTaskPriorityCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
