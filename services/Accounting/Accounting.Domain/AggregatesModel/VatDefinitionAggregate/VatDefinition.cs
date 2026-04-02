namespace Accounting.Domain.AggregatesModel.VatDefinitionAggregate;

public class VatDefinition
{
    public Guid Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }

    public string Name { get; set; } = string.Empty;

    /// <summary>Rate in percent (e.g. 18 for 18%).</summary>
    public decimal Percent { get; set; }

    public bool IsAlcohol { get; set; }

    public string? BuyingAccountName { get; set; }
    /// <summary>Buying-side ledger / account code (UI “recount”).</summary>
    public string BuyingAccountCode { get; set; } = string.Empty;

    public string? SellingAccountName { get; set; }
    public string SellingAccountCode { get; set; } = string.Empty;

    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}
