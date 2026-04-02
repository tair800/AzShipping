using General.Domain.AggregatesModel.VasAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class VasRepository(GeneralDbContext context) : IVasRepository
{
    public async Task<Vas?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Vas.Include(x => x.Currency).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Vas>> GetAllAsync(bool? isActive, bool? isDeleted, CancellationToken cancellationToken = default)
    {
        var query = context.Vas.Include(x => x.Currency).AsQueryable();
        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);
        if (isDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == isDeleted.Value);
        return await query.OrderBy(x => x.Code ?? x.Name ?? "").ToListAsync(cancellationToken);
    }

    public async Task<Vas> AddAsync(Vas entity, CancellationToken cancellationToken = default)
    {
        await context.Vas.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Vas entity, CancellationToken cancellationToken = default)
    {
        context.Vas.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Vas.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Vas.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Vas.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            context.Vas.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
