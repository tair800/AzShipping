using General.Application.DTOs.MeetingHistory;
using General.Domain.AggregatesModel.MeetingHistoryAggregate;

namespace General.Application.Features.MeetingHistories;

public static class MeetingHistoryMapper
{
    public static MeetingHistoryDto MapToDto(MeetingHistory? entity)
    {
        if (entity == null) return null!;
        return new MeetingHistoryDto
        {
            Id = entity.Id,
            MeetingId = entity.MeetingId,
            EventType = entity.EventType,
            Date = entity.Date,
            Time = entity.Time,
            EventResultId = entity.EventResultId,
            FieldName = entity.FieldName,
            OldValue = entity.OldValue,
            NewValue = entity.NewValue,
            CreatedAt = entity.CreatedAt
        };
    }
}
