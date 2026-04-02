using General.Application.DTOs.Task;
using General.Application.Features.Tasks;
using General.Application.Services;
using General.Domain.AggregatesModel.ProjectAggregate;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Tasks.Commands.Create;

public class CreateTaskCommandHandler(ITaskRepository taskRepo, IProjectRepository projectRepo, IActionLogClient actionLogClient)
    : IRequestHandler<CreateTaskCommand, TaskDto>
{
    public async System.Threading.Tasks.Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        Guid? projectId = dto.ProjectId;
        if (projectId.HasValue)
        {
            var project = await projectRepo.GetByIdAsync(projectId.Value, cancellationToken);
            if (project == null) throw new InvalidOperationException($"Project {projectId} not found.");
        }

        var seq = await taskRepo.GetNextTaskSequenceAsync(cancellationToken);
        var taskNo = seq.ToString();

        var entity = new GeneralTask
        {
            Id = Guid.NewGuid(),
            TaskNo = taskNo,
            DateOfCreation = DateTime.UtcNow,
            TaskType = (TaskType)dto.TaskType,
            TaskName = dto.TaskName.Trim(),
            ResponsibleUserId = dto.ResponsibleUserId,
            PriorityId = dto.PriorityId,
            StatusId = dto.StatusId,
            Deadline = dto.Deadline,
            RemindAt = dto.RemindAt,
            Comments = dto.Comments
        };

        TaskInternalRelation.ApplyForCreate(
            entity,
            entity.TaskType,
            dto.RelatedModule,
            dto.RelatedRecordId,
            dto.OperationId,
            dto.ProjectId,
            dto.ClientId);

        await taskRepo.AddAsync(entity, cancellationToken);
        var created = await taskRepo.GetByIdAsync(entity.Id, cancellationToken);
        var result = TaskMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Task created", $"task: {entity.TaskName} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
