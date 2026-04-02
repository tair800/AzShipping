namespace Carrier.Domain.AggregatesModel.RailwayStationAggregate;

public class RailwayStation
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Railway { get; set; }
    public string? LocalName { get; set; }
    public string? Prefix { get; set; }
    public string? Website { get; set; }
    public string? VatNo { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}
