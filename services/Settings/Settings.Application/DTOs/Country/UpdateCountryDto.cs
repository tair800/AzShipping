namespace Settings.Application.DTOs.Country;

public record UpdateCountryDto(
    string IsoCode,
    string Name,
    string? LocalName,
    bool IsStateRequired,
    bool HasCities,
    string Status,
    string? Notes,
    List<Guid>? GlobalZoneIds);
