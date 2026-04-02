namespace Settings.Application.DTOs.State;

public record UpdateStateDto(
    string Code,
    string Name,
    string? LocalName,
    Guid? CountryId,
    string Status,
    string? Notes);
