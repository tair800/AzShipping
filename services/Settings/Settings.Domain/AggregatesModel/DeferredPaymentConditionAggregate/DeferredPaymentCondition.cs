namespace Settings.Domain.AggregatesModel.DeferredPaymentConditionAggregate;

public class DeferredPaymentCondition
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool ClientIncluded { get; set; }
    public int? ClientDaysOfDelay { get; set; }
    public bool CarrierIncluded { get; set; }
    public int? CarrierDaysOfDelay { get; set; }
    public string? FullText { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
