using Carrier.Application.DTOs.CarrierTask;
using Carrier.Application.Features.CarrierTasks;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Commands.Create;

public class CreateCarrierTaskCommandHandler(ITaskRepository taskRepo, IProjectRepository projectRepo, IActionLogClient actionLogClient)
    : IRequestHandler<CreateCarrierTaskCommand, CarrierTaskDto>
{
    public async Task<CarrierTaskDto> Handle(CreateCarrierTaskCommand request, CancellationToken cancellationToken)
    {
        var projectId = request.Dto.ProjectId;
        if (!projectId.HasValue)
        {
            var projects = await projectRepo.GetByCarrierIdAsync(request.CarrierId, cancellationToken);
            var def = projects.FirstOrDefault(p => p.Name.Equals("Default", StringComparison.OrdinalIgnoreCase));
            if (def == null)
            {
                def = await projectRepo.AddAsync(new Project
                {
                    Id = Guid.NewGuid(),
                    CarrierId = request.CarrierId,
                    Name = "Default",
                    CreatedAt = DateTime.UtcNow
                }, cancellationToken);
            }
            projectId = def.Id;
        }
        var seq = await taskRepo.GetNextTaskSequenceAsync(projectId.Value, cancellationToken);
        var taskNo = $"TASK-{seq:D4}";
        var dto = request.Dto;
        var entity = new ProjectTask
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId.Value,
            TaskNo = taskNo,
            DateOfCreation = DateTime.UtcNow,
            ResponsibleUserId = dto.ResponsibleUserId,
            TaskName = dto.TaskName,
            TaskPriorityId = dto.TaskPriorityId,
            TaskStatusId = dto.TaskStatusId,
            Deadline = dto.Deadline
        };
        await taskRepo.AddAsync(entity, cancellationToken);
        var created = await taskRepo.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier task created", $"carrier task: {entity.TaskName} • id: {entity.Id}", null, null, cancellationToken);
        return CarrierTaskMapper.MapToDto(created!);
    }
}
