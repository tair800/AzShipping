namespace Request.Application.DTOs.PriceProposal;

public record CreatePriceProposalDto(
    Guid RequestId,
    string Type = "Calculation",
    string? TemplateName = null,
    Guid? CarrierId = null,
    string? CarrierName = null,
    string? TypeOfService = null,
    string Name = "",
    decimal? ClientPrice = null,
    decimal? ClientPriceWithVat = null,
    Guid? ClientVatRateId = null,
    string? ClientVatRateCode = null,
    Guid? ClientCurrencyId = null,
    string? ClientCurrencyCode = null,
    bool SeparateLineInInvoice = false,
    decimal? CarrierRate = null,
    decimal? CarrierRateWithVat = null,
    Guid? CarrierVatRateId = null,
    string? CarrierVatRateCode = null,
    Guid? CarrierCurrencyId = null,
    string? CarrierCurrencyCode = null,
    decimal? Expense = null,
    decimal? Profit = null,
    string? Route = null,
    string? Comments = null,
    IReadOnlyList<CreatePriceProposalCargoDto>? CargoItems = null);

public record CreatePriceProposalCargoDto(
    string? Description,
    int? Quantity,
    string? PackageType,
    bool IncludeInsurance,
    string? DescriptionOfGoods);
