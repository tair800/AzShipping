using General.Application.Services;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Tasks.Commands.Delete;

public class DeleteTaskCommandHandler(ITaskRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<DeleteTaskCommand, bool>
{
    public async System.Threading.Tasks.Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;

        var taskName = existing.TaskName;
        var id = existing.Id;

        await repository.DeleteAsync(request.Id, cancellationToken);

        await actionLogClient.LogAsync("Task deleted", $"task: {taskName} • id: {id}", null, null, cancellationToken);
        return true;
    }
}
