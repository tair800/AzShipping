using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Domain.AggregatesModel.CountryAggregate;

public class Country
{
    public Guid Id { get; set; }
    public string IsoCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? LocalName { get; set; }
    public bool IsStateRequired { get; set; }
    public bool HasCities { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<CountryGlobalZone> CountryGlobalZones { get; set; } = new List<CountryGlobalZone>();
}
