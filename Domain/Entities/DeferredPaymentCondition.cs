namespace AzShipping.Domain.Entities;

public class DeferredPaymentCondition
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Short name (e.g., "Bit 10 calendar days.")
    public string FullText { get; set; } = string.Empty; // Full description
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

