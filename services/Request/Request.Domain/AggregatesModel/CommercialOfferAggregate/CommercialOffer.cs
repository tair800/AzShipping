namespace Request.Domain.AggregatesModel.CommercialOfferAggregate;

/// <summary>
/// Formal commercial offer for a client, optionally built from selected price calculations (proposals).
/// </summary>
public class CommercialOffer
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public bool ProvideClientAccess { get; set; }
    public Guid? TemplateId { get; set; }
    public string? TemplateName { get; set; }
    public bool BasedOnCalculation { get; set; } = true;
    public string DocumentName { get; set; } = string.Empty;
    /// <summary>Template = generated from Settings template; AttachedFile = external upload reference.</summary>
    public string DocumentSourceType { get; set; } = "Template";
    public string? AttachedFileReference { get; set; }
    public string? Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? UserId { get; set; }
    public string? UserName { get; set; }

    public ICollection<CommercialOfferSelectedProposal> SelectedProposals { get; set; } = [];
}
