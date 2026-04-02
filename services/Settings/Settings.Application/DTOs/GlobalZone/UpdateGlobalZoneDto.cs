namespace Settings.Application.DTOs.GlobalZone;

public record UpdateGlobalZoneDto(
    string Code,
    string Name,
    string? LocalName,
    Guid? CountryId,
    string Status);
