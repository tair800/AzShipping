using Settings.Application.DTOs.MeetingType;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;

namespace Settings.Application.Features.MeetingTypes;

public static class MeetingTypeMapper
{
    public static MeetingTypeDto MapToDto(MeetingType? entity)
    {
        if (entity == null) return null!;
        return new MeetingTypeDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
