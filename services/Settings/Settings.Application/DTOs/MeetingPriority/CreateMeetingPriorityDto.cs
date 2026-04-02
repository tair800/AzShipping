namespace Settings.Application.DTOs.MeetingPriority;

public record CreateMeetingPriorityDto(string Name, string PrimaryColor, string SecondaryColor, bool IsActive);
