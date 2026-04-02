namespace Settings.Application.DTOs.TaskPriority;

public record CreateTaskPriorityDto(string Name, string PrimaryColor, string SecondaryColor, bool IsActive);
