using Clients.Domain.AggregatesModel.NegotiationAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence.Repositories;

public class NegotiationRepository(ClientsDbContext context) : INegotiationRepository
{
    public async Task<Negotiation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Negotiations.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Negotiation>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default)
        => await context.Negotiations.Where(n => n.ClientId == clientId).OrderByDescending(n => n.CreationDate).ToListAsync(cancellationToken);

    public async Task<Negotiation> AddAsync(Negotiation entity, CancellationToken cancellationToken = default)
    {
        context.Negotiations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Negotiation entity, CancellationToken cancellationToken = default)
    {
        var entry = context.Entry(entity);
        if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            context.Negotiations.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Negotiations.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Negotiations.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
