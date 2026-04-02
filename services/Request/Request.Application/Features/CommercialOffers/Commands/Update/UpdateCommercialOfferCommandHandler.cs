using MediatR;
using Request.Application.DTOs.CommercialOffer;
using Request.Application.Features.CommercialOffers;
using Request.Application.Features.CommercialOffers.Commands.Create;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.CommercialOffers.Commands.Update;

public sealed class UpdateCommercialOfferCommandHandler(
    ICommercialOfferRepository commercialOfferRepository,
    IPriceProposalRepository priceProposalRepository) : IRequestHandler<UpdateCommercialOfferCommand, CommercialOfferDto?>
{
    public async Task<CommercialOfferDto?> Handle(UpdateCommercialOfferCommand request, CancellationToken cancellationToken)
    {
        var current = await commercialOfferRepository.GetByIdAsync(request.Id, cancellationToken);
        if (current == null)
            return null;

        var d = request.Dto;
        if (string.IsNullOrWhiteSpace(d.DocumentName))
            throw new InvalidOperationException("Document name is required.");
        var sourceType = string.IsNullOrWhiteSpace(d.DocumentSourceType) ? "Template" : d.DocumentSourceType.Trim();
        if (sourceType.Equals("Template", StringComparison.OrdinalIgnoreCase) && !d.TemplateId.HasValue)
            throw new InvalidOperationException("Template is required when document type is Template.");
        if (sourceType.Equals("AttachedFile", StringComparison.OrdinalIgnoreCase) &&
            string.IsNullOrWhiteSpace(d.AttachedFileReference))
            throw new InvalidOperationException("Attached file reference is required when document type is Attached file.");

        var selectedIds = d.SelectedPriceProposalIds ?? [];
        if (selectedIds.Count > 0)
        {
            await CreateCommercialOfferCommandHandler.EnsureProposalsBelongToRequestAsync(
                current.RequestId, selectedIds, priceProposalRepository, cancellationToken);
        }

        var order = 0;
        var lines = selectedIds.Distinct().Select(pid => new CommercialOfferSelectedProposal
        {
            PriceProposalId = pid,
            SortOrder = order++
        }).ToList();

        var patch = new CommercialOffer
        {
            Id = request.Id,
            ProvideClientAccess = d.ProvideClientAccess,
            TemplateId = d.TemplateId,
            TemplateName = d.TemplateName,
            BasedOnCalculation = d.BasedOnCalculation,
            DocumentName = d.DocumentName.Trim(),
            DocumentSourceType = sourceType.Equals("AttachedFile", StringComparison.OrdinalIgnoreCase) ? "AttachedFile" : "Template",
            AttachedFileReference = string.IsNullOrWhiteSpace(d.AttachedFileReference) ? null : d.AttachedFileReference.Trim(),
            Comments = string.IsNullOrWhiteSpace(d.Comments) ? null : d.Comments.Trim(),
            SelectedProposals = lines
        };

        await commercialOfferRepository.UpdateAsync(patch, cancellationToken);
        var loaded = await commercialOfferRepository.GetByIdAsync(request.Id, cancellationToken);
        return loaded == null ? null : CommercialOfferMapper.MapToDto(loaded);
    }
}
