namespace AzShipping.Application.DTOs;

public class TransportTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Default name (EN or first available)
    public bool IsAir { get; set; }
    public bool IsSea { get; set; }
    public bool IsRoad { get; set; }
    public bool IsRail { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, string> Translations { get; set; } = new(); // LanguageCode -> Name
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

