using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.TransportTypeAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class TransportTypeRepository(SettingsDbContext context) : ITransportTypeRepository
{
    public async Task<TransportType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.TransportTypes.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<TransportType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.TransportTypes.OrderBy(t => t.Name).ToListAsync(cancellationToken);

    public async Task<TransportType> AddAsync(TransportType entity, CancellationToken cancellationToken = default)
    {
        context.TransportTypes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TransportType entity, CancellationToken cancellationToken = default)
    {
        context.TransportTypes.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.TransportTypes.FindAsync([id], cancellationToken);
        if (e != null) { context.TransportTypes.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
