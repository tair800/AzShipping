namespace Accounting.Application.DTOs.VatDefinition;

public record UpdateVatDefinitionDto
{
    public string Name { get; init; } = string.Empty;
    public decimal Percent { get; init; }
    public bool IsAlcohol { get; init; }
    public string? BuyingAccountName { get; init; }
    public string BuyingAccountCode { get; init; } = string.Empty;
    public string? SellingAccountName { get; init; }
    public string SellingAccountCode { get; init; } = string.Empty;
    public string? Notes { get; init; }
    public bool IsActive { get; init; } = true;
}
