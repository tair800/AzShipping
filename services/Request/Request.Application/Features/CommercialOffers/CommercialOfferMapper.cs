using Request.Application.DTOs.CommercialOffer;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;

namespace Request.Application.Features.CommercialOffers;

public static class CommercialOfferMapper
{
    public static CommercialOfferDto MapToDto(CommercialOffer entity)
    {
        var selected = (entity.SelectedProposals ?? []).OrderBy(s => s.SortOrder).ToList();
        var ids = selected.Select(s => s.PriceProposalId).ToList();
        var lines = selected.Select(s =>
        {
            var p = s.PriceProposal;
            return p == null
                ? new CommercialOfferIncludedCalculationDto(s.PriceProposalId, "—", null, null, null, null, null, null)
                : new CommercialOfferIncludedCalculationDto(
                    p.Id,
                    p.Name,
                    p.ClientPrice,
                    p.Expense,
                    p.Profit,
                    p.CarrierName,
                    p.Route,
                    p.Comments);
        }).ToList();

        return new CommercialOfferDto(
            entity.Id,
            entity.RequestId,
            entity.ProvideClientAccess,
            entity.TemplateId,
            entity.TemplateName,
            entity.BasedOnCalculation,
            entity.DocumentName,
            entity.DocumentSourceType,
            entity.AttachedFileReference,
            entity.Comments,
            entity.CreatedAt,
            entity.UserId,
            entity.UserName,
            ids,
            lines);
    }
}
