namespace Settings.Application.DTOs.MeetingStatus;

public record CreateMeetingStatusDto(string Name, string PrimaryColor, string SecondaryColor, bool IsActive);
