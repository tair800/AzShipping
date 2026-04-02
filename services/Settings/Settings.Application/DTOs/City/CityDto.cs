namespace Settings.Application.DTOs.City;

public record CityDto(
    Guid Id,
    string Code,
    string Name,
    string? LocalName,
    Guid? StateId,
    string? StateName,
    string? CountryName,
    string? ZipCode,
    string Status,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
