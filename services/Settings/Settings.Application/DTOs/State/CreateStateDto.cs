namespace Settings.Application.DTOs.State;

public record CreateStateDto(
    string Code,
    string Name,
    string? LocalName,
    Guid? CountryId,
    string Status,
    string? Notes);
