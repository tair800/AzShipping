namespace Settings.Application.DTOs.RequestPurpose;

public record RequestPurposeDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
