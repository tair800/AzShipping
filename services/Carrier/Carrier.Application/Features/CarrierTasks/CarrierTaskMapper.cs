using Carrier.Application.DTOs.CarrierTask;
using Carrier.Domain.AggregatesModel.CarrierAggregate;

namespace Carrier.Application.Features.CarrierTasks;

public static class CarrierTaskMapper
{
    public static CarrierTaskDto MapToDto(ProjectTask? entity)
    {
        if (entity == null) return default!;
        var timer = (string?)null;
        if (entity.Deadline.HasValue)
        {
            var remaining = entity.Deadline.Value.ToUniversalTime() - DateTime.UtcNow;
            if (remaining.TotalSeconds > 0)
                timer = $"{(int)remaining.TotalHours:D2}:{remaining.Minutes:D2}:{remaining.Seconds:D2}";
        }
        return new CarrierTaskDto
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            ProjectName = entity.Project?.Name ?? "",
            CarrierId = entity.Project?.CarrierId ?? Guid.Empty,
            TaskNo = entity.TaskNo,
            DateOfCreation = entity.DateOfCreation,
            ResponsibleUserId = entity.ResponsibleUserId,
            TaskName = entity.TaskName,
            TaskPriorityId = entity.TaskPriorityId,
            TaskStatusId = entity.TaskStatusId,
            Deadline = entity.Deadline,
            TimerCountdown = timer
        };
    }
}
