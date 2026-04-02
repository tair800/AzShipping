using Settings.Domain.AggregatesModel.ResultTypeAggregate;

namespace Settings.Domain.AggregatesModel.FunnelResultAggregate;

public class FunnelResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid ResultTypeId { get; set; }
    public ResultType ResultType { get; set; } = null!;
    public bool ToNextStep { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
