using MediatR;
using Request.Application.DTOs.CommercialOffer;
using Request.Application.Features.CommercialOffers;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.CommercialOffers.Commands.Create;

public sealed class CreateCommercialOfferCommandHandler(
    ICommercialOfferRepository commercialOfferRepository,
    IPriceProposalRepository priceProposalRepository) : IRequestHandler<CreateCommercialOfferCommand, CommercialOfferDto>
{
    public async Task<CommercialOfferDto> Handle(CreateCommercialOfferCommand request, CancellationToken cancellationToken)
    {
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
            await EnsureProposalsBelongToRequestAsync(d.RequestId, selectedIds, priceProposalRepository, cancellationToken);

        var now = DateTime.UtcNow;
        var entity = new CommercialOffer
        {
            Id = Guid.NewGuid(),
            RequestId = d.RequestId,
            ProvideClientAccess = d.ProvideClientAccess,
            TemplateId = d.TemplateId,
            TemplateName = d.TemplateName,
            BasedOnCalculation = d.BasedOnCalculation,
            DocumentName = d.DocumentName.Trim(),
            DocumentSourceType = sourceType.Equals("AttachedFile", StringComparison.OrdinalIgnoreCase) ? "AttachedFile" : "Template",
            AttachedFileReference = string.IsNullOrWhiteSpace(d.AttachedFileReference) ? null : d.AttachedFileReference.Trim(),
            Comments = string.IsNullOrWhiteSpace(d.Comments) ? null : d.Comments.Trim(),
            CreatedAt = now
        };

        var order = 0;
        foreach (var pid in selectedIds.Distinct())
        {
            entity.SelectedProposals.Add(new CommercialOfferSelectedProposal
            {
                Id = Guid.NewGuid(),
                CommercialOfferId = entity.Id,
                PriceProposalId = pid,
                SortOrder = order++
            });
        }

        await commercialOfferRepository.AddAsync(entity, cancellationToken);
        var loaded = await commercialOfferRepository.GetByIdAsync(entity.Id, cancellationToken);
        return CommercialOfferMapper.MapToDto(loaded ?? entity);
    }

    public static async Task EnsureProposalsBelongToRequestAsync(
        Guid requestId,
        IReadOnlyList<Guid> selectedIds,
        IPriceProposalRepository priceProposalRepository,
        CancellationToken cancellationToken)
    {
        var proposals = await priceProposalRepository.GetByRequestIdAsync(requestId, cancellationToken);
        var allowed = proposals.Select(p => p.Id).ToHashSet();
        foreach (var id in selectedIds)
        {
            if (!allowed.Contains(id))
                throw new InvalidOperationException($"Price proposal {id} does not belong to this request.");
        }
    }
}
