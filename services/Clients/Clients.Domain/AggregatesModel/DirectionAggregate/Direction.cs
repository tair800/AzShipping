namespace Clients.Domain.AggregatesModel.DirectionAggregate;

public class Direction
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid? FromCountryId { get; set; }
    public Guid? FromCityId { get; set; }
    public Guid? ToCountryId { get; set; }
    public Guid? ToCityId { get; set; }
    public string? Note { get; set; }
    public string? Comments { get; set; }
}
