using General.Domain.AggregatesModel.VesselAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class VesselRepository(GeneralDbContext context) : IVesselRepository
{
    public async Task<Vessel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Vessels.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Vessel>> GetAllAsync(bool? isActive, bool? isDeleted, CancellationToken cancellationToken = default)
    {
        var query = context.Vessels.AsQueryable();
        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);
        if (isDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == isDeleted.Value);
        return await query.OrderBy(x => x.Name ?? x.Code ?? "").ToListAsync(cancellationToken);
    }

    public async Task<Vessel> AddAsync(Vessel entity, CancellationToken cancellationToken = default)
    {
        await context.Vessels.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Vessel entity, CancellationToken cancellationToken = default)
    {
        context.Vessels.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Vessels.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Vessels.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Vessels.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            context.Vessels.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
