using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class PriceProposalRepository(RequestDbContext context) : IPriceProposalRepository
{
    public async Task<PriceProposal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.PriceProposals
            .Include(x => x.CargoItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<PriceProposal>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
        => await context.PriceProposals
            .Include(x => x.CargoItems)
            .Where(x => x.RequestId == requestId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

    public async Task<PriceProposal> AddAsync(PriceProposal entity, CancellationToken cancellationToken = default)
    {
        context.PriceProposals.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(PriceProposal entity, CancellationToken cancellationToken = default)
    {
        var oldCargo = await context.PriceProposalCargos.Where(c => c.PriceProposalId == entity.Id).ToListAsync(cancellationToken);
        if (oldCargo.Count > 0)
            context.PriceProposalCargos.RemoveRange(oldCargo);
        context.PriceProposals.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.PriceProposals.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.PriceProposals.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
