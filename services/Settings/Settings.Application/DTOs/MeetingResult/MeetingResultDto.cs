namespace Settings.Application.DTOs.MeetingResult;

public record MeetingResultDto(Guid Id, string Name, string PrimaryColor, string SecondaryColor, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
