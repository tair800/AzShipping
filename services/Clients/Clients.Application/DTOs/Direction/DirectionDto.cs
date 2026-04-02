namespace Clients.Application.DTOs.Direction;

public record DirectionDto
{
    public Guid Id { get; init; }
    public Guid ClientId { get; init; }
    public Guid? FromCountryId { get; init; }
    public Guid? FromCityId { get; init; }
    public Guid? ToCountryId { get; init; }
    public Guid? ToCityId { get; init; }
    public string? Note { get; init; }
    public string? Comments { get; init; }
}
