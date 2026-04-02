using Settings.Application.DTOs.TaskPriority;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;

namespace Settings.Application.Features.TaskPriorities;

public static class TaskPriorityMapper
{
    public static TaskPriorityDto MapToDto(TaskPriority? entity)
    {
        if (entity == null) return null!;
        return new TaskPriorityDto(entity.Id, entity.Name, entity.PrimaryColor, entity.SecondaryColor, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
