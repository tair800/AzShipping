namespace Settings.Application.DTOs.Uom;

public record UomDto(Guid Id, string Name, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
