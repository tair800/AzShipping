namespace Clients.Application.DTOs.Direction;

public record UpdateDirectionDto(
    Guid? FromCountryId,
    Guid? FromCityId,
    Guid? ToCountryId,
    Guid? ToCityId,
    string? Note,
    string? Comments);
