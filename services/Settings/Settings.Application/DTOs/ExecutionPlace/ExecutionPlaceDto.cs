namespace Settings.Application.DTOs.ExecutionPlace;

public record ExecutionPlaceDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
