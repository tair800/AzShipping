namespace Settings.Application.DTOs.CarrierType;

public record CarrierTypeDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
