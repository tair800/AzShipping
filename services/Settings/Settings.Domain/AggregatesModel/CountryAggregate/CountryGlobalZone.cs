using Settings.Domain.AggregatesModel.GlobalZoneAggregate;

namespace Settings.Domain.AggregatesModel.CountryAggregate;

public class CountryGlobalZone
{
    public Guid CountryId { get; set; }
    public Country Country { get; set; } = null!;
    
    public Guid GlobalZoneId { get; set; }
    public GlobalZone GlobalZone { get; set; } = null!;
}
