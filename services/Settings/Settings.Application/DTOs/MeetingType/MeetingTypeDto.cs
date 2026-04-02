namespace Settings.Application.DTOs.MeetingType;

public record MeetingTypeDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
