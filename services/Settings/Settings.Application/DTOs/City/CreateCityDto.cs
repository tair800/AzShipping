namespace Settings.Application.DTOs.City;

public record CreateCityDto(
    string Code,
    string Name,
    string? LocalName,
    Guid? StateId,
    string? ZipCode,
    string Status,
    string? Notes);
