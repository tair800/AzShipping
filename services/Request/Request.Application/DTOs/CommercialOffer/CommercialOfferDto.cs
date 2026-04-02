namespace Request.Application.DTOs.CommercialOffer;

public record CommercialOfferIncludedCalculationDto(
    Guid PriceProposalId,
    string Name,
    decimal? ClientPrice,
    decimal? Expense,
    decimal? Profit,
    string? ContractPartnerName,
    string? Route,
    string? Comment);

public record CommercialOfferDto(
    Guid Id,
    Guid RequestId,
    bool ProvideClientAccess,
    Guid? TemplateId,
    string? TemplateName,
    bool BasedOnCalculation,
    string DocumentName,
    string DocumentSourceType,
    string? AttachedFileReference,
    string? Comments,
    DateTime CreatedAt,
    Guid? UserId,
    string? UserName,
    IReadOnlyList<Guid> SelectedPriceProposalIds,
    IReadOnlyList<CommercialOfferIncludedCalculationDto> IncludedCalculations);
