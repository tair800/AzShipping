using MediatR;
using Settings.Application.DTOs.TaskStatus;
using Settings.Application.Features.TaskStatuses;
using Settings.Domain.AggregatesModel.TaskStatusAggregate;

namespace Settings.Application.Features.TaskStatuses.Commands.Update;

public sealed class UpdateTaskStatusCommandHandler(ITaskStatusRepository repository) : IRequestHandler<UpdateTaskStatusCommand, TaskStatusDto?>
{
    public async Task<TaskStatusDto?> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.PrimaryColor = request.Dto.PrimaryColor;
        entity.SecondaryColor = request.Dto.SecondaryColor;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return TaskStatusMapper.MapToDto(entity);
    }
}
