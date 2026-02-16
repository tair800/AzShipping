namespace AzShipping.Domain.Entities;

public class SalesFunnelStatus
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int StatusPosition { get; set; }
    public Guid? ResponsibleManagerId { get; set; }
    public User? ResponsibleManager { get; set; }
    public int NumberOfDays { get; set; }
    public bool SendToEmail { get; set; }
    public bool SendNotification { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<SalesFunnelStatusTranslation> Translations { get; set; } = new List<SalesFunnelStatusTranslation>();
}

