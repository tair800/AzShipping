namespace Request.Application.DTOs.CommercialOffer;

public record UpdateCommercialOfferDto(
    string DocumentName,
    bool ProvideClientAccess = false,
    Guid? TemplateId = null,
    string? TemplateName = null,
    bool BasedOnCalculation = true,
    string DocumentSourceType = "Template",
    string? AttachedFileReference = null,
    string? Comments = null,
    IReadOnlyList<Guid>? SelectedPriceProposalIds = null);
