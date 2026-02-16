namespace AzShipping.Domain.Entities;

public class TransportTypeTranslation
{
    public Guid Id { get; set; }
    public Guid TransportTypeId { get; set; }
    public TransportType TransportType { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty; // LT, EN, RU, FR, UK, PL, KA, AZ
    public string Name { get; set; } = string.Empty;
}

