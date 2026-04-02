namespace Settings.Application.DTOs.TaskStatus;

public record UpdateTaskStatusDto(string Name, string PrimaryColor, string SecondaryColor, bool IsActive);
