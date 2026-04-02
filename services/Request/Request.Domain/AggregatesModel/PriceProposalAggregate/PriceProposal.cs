namespace Request.Domain.AggregatesModel.PriceProposalAggregate;

public class PriceProposal
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public string Type { get; set; } = "Calculation"; // Calculation | Service
    public string? TemplateName { get; set; }
    public Guid? CarrierId { get; set; }
    public string? CarrierName { get; set; }
    public string? TypeOfService { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? ClientPrice { get; set; }
    public decimal? ClientPriceWithVat { get; set; }
    public Guid? ClientVatRateId { get; set; }
    public string? ClientVatRateCode { get; set; }
    public Guid? ClientCurrencyId { get; set; }
    public string? ClientCurrencyCode { get; set; }
    public bool SeparateLineInInvoice { get; set; }
    public decimal? CarrierRate { get; set; }
    public decimal? CarrierRateWithVat { get; set; }
    public Guid? CarrierVatRateId { get; set; }
    public string? CarrierVatRateCode { get; set; }
    public Guid? CarrierCurrencyId { get; set; }
    public string? CarrierCurrencyCode { get; set; }
    public decimal? Expense { get; set; }
    public decimal? Profit { get; set; }
    public string? Route { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }

    public ICollection<PriceProposalCargo> CargoItems { get; set; } = [];
}
