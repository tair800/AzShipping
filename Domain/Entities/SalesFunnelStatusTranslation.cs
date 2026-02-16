namespace AzShipping.Domain.Entities;

public class SalesFunnelStatusTranslation
{
    public Guid Id { get; set; }
    public Guid SalesFunnelStatusId { get; set; }
    public SalesFunnelStatus SalesFunnelStatus { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty; // LT, EN, RU, FR, UK, PL, KA, AZ
    public string Name { get; set; } = string.Empty;
}

