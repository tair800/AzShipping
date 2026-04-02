namespace Settings.Domain.AggregatesModel.PackagingAggregate;

public class Packaging
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<PackagingTranslation> Translations { get; set; } = new List<PackagingTranslation>();
}

public class PackagingTranslation
{
    public Guid Id { get; set; }
    public Guid PackagingId { get; set; }
    public Packaging Packaging { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
