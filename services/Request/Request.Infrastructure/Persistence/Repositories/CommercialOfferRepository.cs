using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class CommercialOfferRepository(RequestDbContext context) : ICommercialOfferRepository
{
    public async Task<CommercialOffer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.CommercialOffers
            .Include(x => x.SelectedProposals)
            .ThenInclude(s => s.PriceProposal)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<CommercialOffer>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
        => await context.CommercialOffers
            .Include(x => x.SelectedProposals)
            .ThenInclude(s => s.PriceProposal)
            .Where(x => x.RequestId == requestId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

    public async Task<CommercialOffer> AddAsync(CommercialOffer entity, CancellationToken cancellationToken = default)
    {
        context.CommercialOffers.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(CommercialOffer entity, CancellationToken cancellationToken = default)
    {
        var existing = await context.CommercialOffers
            .Include(x => x.SelectedProposals)
            .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
        if (existing == null)
            return;

        existing.ProvideClientAccess = entity.ProvideClientAccess;
        existing.TemplateId = entity.TemplateId;
        existing.TemplateName = entity.TemplateName;
        existing.BasedOnCalculation = entity.BasedOnCalculation;
        existing.DocumentName = entity.DocumentName;
        existing.DocumentSourceType = entity.DocumentSourceType;
        existing.AttachedFileReference = entity.AttachedFileReference;
        existing.Comments = entity.Comments;

        if (existing.SelectedProposals.Count > 0)
            context.CommercialOfferSelectedProposals.RemoveRange(existing.SelectedProposals);

        var order = 0;
        foreach (var line in entity.SelectedProposals.OrderBy(s => s.SortOrder))
        {
            context.CommercialOfferSelectedProposals.Add(new CommercialOfferSelectedProposal
            {
                Id = Guid.NewGuid(),
                CommercialOfferId = existing.Id,
                PriceProposalId = line.PriceProposalId,
                SortOrder = order++
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.CommercialOffers.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.CommercialOffers.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
