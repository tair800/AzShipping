namespace Settings.Application.DTOs.GlobalZone;

public record CreateGlobalZoneDto(
    string Code,
    string Name,
    string? LocalName,
    Guid? CountryId,
    string Status);
