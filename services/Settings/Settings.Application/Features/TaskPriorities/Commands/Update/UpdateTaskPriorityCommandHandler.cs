using MediatR;
using Settings.Application.DTOs.TaskPriority;
using Settings.Application.Features.TaskPriorities;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;

namespace Settings.Application.Features.TaskPriorities.Commands.Update;

public sealed class UpdateTaskPriorityCommandHandler(ITaskPriorityRepository repository) : IRequestHandler<UpdateTaskPriorityCommand, TaskPriorityDto?>
{
    public async Task<TaskPriorityDto?> Handle(UpdateTaskPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.PrimaryColor = request.Dto.PrimaryColor;
        entity.SecondaryColor = request.Dto.SecondaryColor;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return TaskPriorityMapper.MapToDto(entity);
    }
}
