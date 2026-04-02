using General.Application.DTOs.Meeting;
using General.Domain.AggregatesModel.MeetingAggregate;

namespace General.Application.Features.Meetings;

public static class MeetingMapper
{
    public static MeetingDto MapToDto(Meeting? entity)
    {
        if (entity == null) return null!;
        return new MeetingDto
        {
            Id = entity.Id,
            Name = entity.Name,
            MeetingTypeId = entity.MeetingTypeId,
            MeetingResultId = entity.MeetingResultId,
            MeetingStatusId = entity.MeetingStatusId,
            ClientId = entity.ClientId,
            TaskId = entity.TaskId,
            OperationId = entity.OperationId,
            MeetingPriorityId = entity.MeetingPriorityId,
            Date = entity.Date,
            Time = entity.Time,
            Address = entity.Address,
            Comments = entity.Comments,
            HasClient = entity.HasClient,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
