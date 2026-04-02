using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Domain.AggregatesModel.CityAggregate;

public class City
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? LocalName { get; set; }
    public Guid? StateId { get; set; }
    public State? State { get; set; }
    public string? ZipCode { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Active;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
