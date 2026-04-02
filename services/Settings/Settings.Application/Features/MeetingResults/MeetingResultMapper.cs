using Settings.Application.DTOs.MeetingResult;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;

namespace Settings.Application.Features.MeetingResults;

public static class MeetingResultMapper
{
    public static MeetingResultDto MapToDto(MeetingResult? entity)
    {
        if (entity == null) return null!;
        return new MeetingResultDto(entity.Id, entity.Name, entity.PrimaryColor, entity.SecondaryColor, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
