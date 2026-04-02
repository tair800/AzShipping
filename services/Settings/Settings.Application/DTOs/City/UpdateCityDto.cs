namespace Settings.Application.DTOs.City;

public record UpdateCityDto(
    string Code,
    string Name,
    string? LocalName,
    Guid? StateId,
    string? ZipCode,
    string Status,
    string? Notes);
