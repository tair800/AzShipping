using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.UomAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class UomRepository(SettingsDbContext context) : IUomRepository
{
    public async Task<Uom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Uoms.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Uom>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Uoms.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<Uom> AddAsync(Uom entity, CancellationToken cancellationToken = default)
    {
        context.Uoms.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Uom entity, CancellationToken cancellationToken = default)
    {
        context.Uoms.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Uoms.FindAsync([id], cancellationToken);
        if (e != null) { context.Uoms.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
