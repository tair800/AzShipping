using Carrier.Application.DTOs.CarrierTask;
using Carrier.Application.Features.CarrierTasks;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierTasks.Commands.Update;

public class UpdateCarrierTaskCommandHandler(ITaskRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateCarrierTaskCommand, CarrierTaskDto?>
{
    public async Task<CarrierTaskDto?> Handle(UpdateCarrierTaskCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;
        // ProjectId is immutable - task stays in same project

        var dto = request.Dto;
        existing.ResponsibleUserId = dto.ResponsibleUserId;
        existing.TaskName = dto.TaskName;
        existing.TaskPriorityId = dto.TaskPriorityId;
        existing.TaskStatusId = dto.TaskStatusId;
        existing.Deadline = dto.Deadline;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier task updated", $"carrier task: {existing.TaskName} • id: {existing.Id}", null, null, cancellationToken);
        return CarrierTaskMapper.MapToDto(updated!);
    }
}
