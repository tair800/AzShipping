namespace Settings.Application.DTOs.TaskStatus;

public record CreateTaskStatusDto(string Name, string PrimaryColor, string SecondaryColor, bool IsActive);
