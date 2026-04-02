using Settings.Application.DTOs.MeetingStatus;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;

namespace Settings.Application.Features.MeetingStatuses;

public static class MeetingStatusMapper
{
    public static MeetingStatusDto MapToDto(MeetingStatus? entity)
    {
        if (entity == null) return null!;
        return new MeetingStatusDto(entity.Id, entity.Name, entity.PrimaryColor, entity.SecondaryColor, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
