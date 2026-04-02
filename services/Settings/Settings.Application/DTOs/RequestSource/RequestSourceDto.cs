namespace Settings.Application.DTOs.RequestSource;

public record RequestSourceDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
