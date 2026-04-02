using General.Application.DTOs.Task;
using General.Application.Features.Tasks;
using General.Application.Services;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Tasks.Commands.Update;

public class UpdateTaskCommandHandler(ITaskRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateTaskCommand, TaskDto?>
{
    public async System.Threading.Tasks.Task<TaskDto?> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.TaskName = dto.TaskName.Trim();
        TaskInternalRelation.ApplyOnUpdate(existing, dto);
        existing.ResponsibleUserId = dto.ResponsibleUserId;
        existing.PriorityId = dto.PriorityId;
        existing.StatusId = dto.StatusId;
        existing.Deadline = dto.Deadline;
        existing.RemindAt = dto.RemindAt;
        existing.Comments = dto.Comments;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        var result = TaskMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Task updated", $"task: {existing.TaskName} • id: {existing.Id}", null, null, cancellationToken);
        return result;
    }
}
