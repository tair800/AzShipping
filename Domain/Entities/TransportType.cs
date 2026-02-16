namespace AzShipping.Domain.Entities;

public class TransportType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Default name (for backward compatibility)
    public bool IsAir { get; set; }
    public bool IsSea { get; set; }
    public bool IsRoad { get; set; }
    public bool IsRail { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<TransportTypeTranslation> Translations { get; set; } = new List<TransportTypeTranslation>();
}

