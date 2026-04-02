using Settings.Application.DTOs.MeetingPriority;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;

namespace Settings.Application.Features.MeetingPriorities;

public static class MeetingPriorityMapper
{
    public static MeetingPriorityDto MapToDto(MeetingPriority? entity)
    {
        if (entity == null) return null!;
        return new MeetingPriorityDto(entity.Id, entity.Name, entity.PrimaryColor, entity.SecondaryColor, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
