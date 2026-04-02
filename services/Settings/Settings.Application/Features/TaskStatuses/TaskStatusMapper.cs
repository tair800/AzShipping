using Settings.Application.DTOs.TaskStatus;
using TaskStatusEntity = Settings.Domain.AggregatesModel.TaskStatusAggregate.TaskStatus;

namespace Settings.Application.Features.TaskStatuses;

public static class TaskStatusMapper
{
    public static TaskStatusDto MapToDto(TaskStatusEntity? entity)
    {
        if (entity == null) return null!;
        return new TaskStatusDto(entity.Id, entity.Name, entity.PrimaryColor, entity.SecondaryColor, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
