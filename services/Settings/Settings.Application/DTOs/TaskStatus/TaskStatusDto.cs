namespace Settings.Application.DTOs.TaskStatus;

public record TaskStatusDto(Guid Id, string Name, string PrimaryColor, string SecondaryColor, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
