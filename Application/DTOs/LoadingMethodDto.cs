namespace AzShipping.Application.DTOs;

public class LoadingMethodDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Default name (EN or first available)
    public bool IsActive { get; set; }
    public Dictionary<string, string> Translations { get; set; } = new(); // LanguageCode -> Name
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

