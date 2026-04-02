using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.GlobalZoneAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class GlobalZoneRepository(SettingsDbContext context) : IGlobalZoneRepository
{
    public async Task<GlobalZone?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.GlobalZones.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<GlobalZone>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.GlobalZones.Where(x => x.Status != EntityStatus.Deleted).OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<GlobalZone>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default)
        => await context.GlobalZones.Where(x => x.Status == status).OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<GlobalZone> AddAsync(GlobalZone entity, CancellationToken cancellationToken = default)
    {
        await context.GlobalZones.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(GlobalZone entity, CancellationToken cancellationToken = default)
    {
        context.GlobalZones.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            entity.Status = EntityStatus.Deleted;
            entity.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
