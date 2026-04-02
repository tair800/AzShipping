namespace Settings.Application.DTOs.PricingType;

public record PricingTypeDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
