using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.WayOfNegotiationAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class WayOfNegotiationRepository(SettingsDbContext context) : IWayOfNegotiationRepository
{
    public async Task<WayOfNegotiation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.WayOfNegotiations.Include(p => p.Translations).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IReadOnlyList<WayOfNegotiation>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.WayOfNegotiations.Include(p => p.Translations).OrderBy(p => p.Name).ToListAsync(cancellationToken);

    public async Task<WayOfNegotiation> AddAsync(WayOfNegotiation entity, CancellationToken cancellationToken = default)
    {
        context.WayOfNegotiations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(WayOfNegotiation entity, CancellationToken cancellationToken = default)
    {
        context.WayOfNegotiations.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.WayOfNegotiations.Include(p => p.Translations).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (e != null) { context.WayOfNegotiations.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
