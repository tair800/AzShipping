namespace Settings.Domain.AggregatesModel.AddressTypeAggregate;

/// <summary>Address type (e.g. Main Address, Warehouse) for Edit Address popup.</summary>
public class AddressType
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
}
