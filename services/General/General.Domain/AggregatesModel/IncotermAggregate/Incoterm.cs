namespace General.Domain.AggregatesModel.IncotermAggregate;

public class Incoterm
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? LocalName { get; set; }
    /// <summary>Freight option: Collect or Prepaid</summary>
    public string? Freight { get; set; }
    /// <summary>Other charges option: Collect or Prepaid</summary>
    public string? OtherCharges { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }
}
