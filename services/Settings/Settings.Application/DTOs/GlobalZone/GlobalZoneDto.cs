namespace Settings.Application.DTOs.GlobalZone;

public record GlobalZoneDto(
    Guid Id,
    string Code,
    string Name,
    string? LocalName,
    Guid? CountryId,
    string? CountryName,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
