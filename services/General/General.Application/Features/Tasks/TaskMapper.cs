using General.Application.DTOs.Task;
using General.Domain.AggregatesModel.TaskAggregate;

namespace General.Application.Features.Tasks;

public static class TaskMapper
{
    public static TaskDto MapToDto(GeneralTask? entity)
    {
        if (entity == null) return null!;
        var timer = (string?)null;
        if (entity.Deadline.HasValue)
        {
            var remaining = entity.Deadline.Value.ToUniversalTime() - DateTime.UtcNow;
            if (remaining.TotalSeconds > 0)
                timer = $"{(int)remaining.TotalHours:D2}:{remaining.Minutes:D2}:{remaining.Seconds:D2}";
        }
        return new TaskDto
        {
            Id = entity.Id,
            TaskNo = entity.TaskNo,
            DateOfCreation = entity.DateOfCreation,
            TaskType = (int)entity.TaskType,
            TaskName = entity.TaskName,
            OperationId = entity.OperationId,
            ClientId = entity.ClientId,
            ProjectId = entity.ProjectId,
            ProjectName = entity.Project?.Name ?? "",
            RelatedModule = (int)entity.RelatedModule,
            RelatedRecordId = entity.RelatedRecordId,
            RelatedModuleLabel = TaskInternalRelation.Label(entity.RelatedModule),
            ResponsibleUserId = entity.ResponsibleUserId,
            ResponsiblePersonName = null,  // Resolve from User service when available
            PriorityId = entity.PriorityId,
            StatusId = entity.StatusId,
            Deadline = entity.Deadline,
            RemindAt = entity.RemindAt,
            TimerCountdown = timer,
            Comments = entity.Comments,
            Documents = entity.Documents?.Select(d => new TaskDocumentDto
            {
                Id = d.Id,
                FilePath = d.FilePath,
                DocumentName = d.DocumentName
            }).ToList() ?? []
        };
    }
}
