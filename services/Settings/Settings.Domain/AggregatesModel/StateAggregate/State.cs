using Settings.Domain.AggregatesModel.CountryAggregate;

namespace Settings.Domain.AggregatesModel.StateAggregate;

public class State
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? LocalName { get; set; }
    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
