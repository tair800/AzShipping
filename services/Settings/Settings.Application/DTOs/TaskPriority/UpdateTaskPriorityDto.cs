namespace Settings.Application.DTOs.TaskPriority;

public record UpdateTaskPriorityDto(string Name, string PrimaryColor, string SecondaryColor, bool IsActive);
