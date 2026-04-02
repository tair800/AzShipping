namespace Settings.Application.DTOs.Country;

public record CountryDto(
    Guid Id,
    string IsoCode,
    string Name,
    string? LocalName,
    bool IsStateRequired,
    bool HasCities,
    string Status,
    string? Notes,
    List<GlobalZoneRef> GlobalZones,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

public record GlobalZoneRef(Guid Id, string Code, string Name);
