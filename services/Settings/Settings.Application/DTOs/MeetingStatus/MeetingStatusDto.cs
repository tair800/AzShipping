namespace Settings.Application.DTOs.MeetingStatus;

public record MeetingStatusDto(Guid Id, string Name, string PrimaryColor, string SecondaryColor, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
