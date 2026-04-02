namespace Settings.Application.DTOs.State;

public record StateDto(
    Guid Id,
    string Code,
    string Name,
    string? LocalName,
    Guid? CountryId,
    string? CountryName,
    string Status,
    string? Notes,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
