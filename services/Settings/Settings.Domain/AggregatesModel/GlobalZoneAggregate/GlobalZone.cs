using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Domain.AggregatesModel.GlobalZoneAggregate;

public class GlobalZone
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? LocalName { get; set; }
    public Guid? CountryId { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
