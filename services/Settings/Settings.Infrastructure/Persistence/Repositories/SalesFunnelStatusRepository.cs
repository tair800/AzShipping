using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class SalesFunnelStatusRepository(SettingsDbContext context) : ISalesFunnelStatusRepository
{
    public async Task<SalesFunnelStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.SalesFunnelStatuses.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<SalesFunnelStatus>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.SalesFunnelStatuses.OrderBy(s => s.StatusPosition).ToListAsync(cancellationToken);

    public async Task<SalesFunnelStatus> AddAsync(SalesFunnelStatus entity, CancellationToken cancellationToken = default)
    {
        context.SalesFunnelStatuses.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(SalesFunnelStatus entity, CancellationToken cancellationToken = default)
    {
        context.SalesFunnelStatuses.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.SalesFunnelStatuses.FindAsync([id], cancellationToken);
        if (e != null) { context.SalesFunnelStatuses.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
