namespace Settings.Application.DTOs.MeetingPriority;

public record MeetingPriorityDto(Guid Id, string Name, string PrimaryColor, string SecondaryColor, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
