using Clients.Domain.AggregatesModel.NegotiationAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence.Repositories;

public class NegotiationResultRepository(ClientsDbContext context) : INegotiationResultRepository
{
    public async Task<IReadOnlyList<NegotiationResult>> GetByNegotiationIdAsync(Guid negotiationId, CancellationToken cancellationToken = default)
        => await context.NegotiationResults
            .Where(r => r.NegotiationId == negotiationId)
            .OrderBy(r => r.ResultDate)
            .ToListAsync(cancellationToken);

    public async Task<NegotiationResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.NegotiationResults.FindAsync([id], cancellationToken);

    public async Task<NegotiationResult> AddAsync(NegotiationResult entity, CancellationToken cancellationToken = default)
    {
        context.NegotiationResults.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(NegotiationResult entity, CancellationToken cancellationToken = default)
    {
        var entry = context.Entry(entity);
        if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            context.NegotiationResults.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.NegotiationResults.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.NegotiationResults.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task DeleteByNegotiationIdAsync(Guid negotiationId, CancellationToken cancellationToken = default)
    {
        var list = await context.NegotiationResults.Where(r => r.NegotiationId == negotiationId).ToListAsync(cancellationToken);
        context.NegotiationResults.RemoveRange(list);
        await context.SaveChangesAsync(cancellationToken);
    }
}
