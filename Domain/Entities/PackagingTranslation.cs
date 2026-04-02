namespace AzShipping.Domain.Entities;

public class PackagingTranslation
{
    public Guid Id { get; set; }
    public Guid PackagingId { get; set; }
    public Packaging Packaging { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty; // LT, EN, RU, FR, UK, PL, KA, AZ
    public string Name { get; set; } = string.Empty;
}

