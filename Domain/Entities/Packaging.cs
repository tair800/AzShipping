namespace AzShipping.Domain.Entities;

public class Packaging
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<PackagingTranslation> Translations { get; set; } = new List<PackagingTranslation>();
}

