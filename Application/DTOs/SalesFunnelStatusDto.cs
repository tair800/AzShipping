namespace AzShipping.Application.DTOs;

public class SalesFunnelStatusDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Default name (EN or first available)
    public int StatusPosition { get; set; }
    public Guid? ResponsibleManagerId { get; set; }
    public string? ResponsibleManagerName { get; set; }
    public int NumberOfDays { get; set; }
    public bool SendToEmail { get; set; }
    public bool SendNotification { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, string> Translations { get; set; } = new(); // LanguageCode -> Name
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

