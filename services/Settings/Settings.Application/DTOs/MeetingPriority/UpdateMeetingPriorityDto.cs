namespace Settings.Application.DTOs.MeetingPriority;

public record UpdateMeetingPriorityDto(string Name, string PrimaryColor, string SecondaryColor, bool IsActive);
