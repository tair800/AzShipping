namespace Settings.Application.DTOs.DrivingLicenceCategory;

public record DrivingLicenceCategoryDto(Guid Id, string Name, string? Code, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
