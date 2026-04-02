using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.FunnelResultAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class FunnelResultRepository(SettingsDbContext context) : IFunnelResultRepository
{
    public async Task<FunnelResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.FunnelResults.Include(x => x.ResultType).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<FunnelResult>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.FunnelResults.Include(x => x.ResultType).OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<FunnelResult> AddAsync(FunnelResult entity, CancellationToken cancellationToken = default)
    {
        context.FunnelResults.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(FunnelResult entity, CancellationToken cancellationToken = default)
    {
        context.FunnelResults.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.FunnelResults.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.FunnelResults.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
