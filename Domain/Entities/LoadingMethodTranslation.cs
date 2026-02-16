namespace AzShipping.Domain.Entities;

public class LoadingMethodTranslation
{
    public Guid Id { get; set; }
    public Guid LoadingMethodId { get; set; }
    public LoadingMethod LoadingMethod { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty; // LT, EN, RU, FR, UK, PL, KA, AZ
    public string Name { get; set; } = string.Empty;
}

