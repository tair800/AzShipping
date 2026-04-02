namespace Settings.Application.DTOs.TaskPriority;

public record TaskPriorityDto(Guid Id, string Name, string PrimaryColor, string SecondaryColor, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
