namespace Clients.Application.DTOs.Direction;

public record CreateDirectionDto(
    Guid ClientId,
    Guid? FromCountryId,
    Guid? FromCityId,
    Guid? ToCountryId,
    Guid? ToCityId,
    string? Note,
    string? Comments);
