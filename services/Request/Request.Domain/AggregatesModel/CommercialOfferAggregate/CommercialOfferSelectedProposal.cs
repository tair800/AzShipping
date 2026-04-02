using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Domain.AggregatesModel.CommercialOfferAggregate;

public class CommercialOfferSelectedProposal
{
    public Guid Id { get; set; }
    public Guid CommercialOfferId { get; set; }
    public CommercialOffer CommercialOffer { get; set; } = null!;
    public Guid PriceProposalId { get; set; }
    public PriceProposal? PriceProposal { get; set; }
    public int SortOrder { get; set; }
}
