using General.Domain.AggregatesModel.IncotermAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class IncotermRepository(GeneralDbContext context) : IIncotermRepository
{
    public async Task<Incoterm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Incoterms.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Incoterm>> GetAllAsync(bool? isActive, bool? isDeleted, CancellationToken cancellationToken = default)
    {
        var query = context.Incoterms.AsQueryable();
        if (isActive.HasValue)
            query = query.Where(x => x.IsActive == isActive.Value);
        if (isDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == isDeleted.Value);
        return await query.OrderBy(x => x.Code ?? x.Name ?? "").ToListAsync(cancellationToken);
    }

    public async Task<Incoterm> AddAsync(Incoterm entity, CancellationToken cancellationToken = default)
    {
        await context.Incoterms.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Incoterm entity, CancellationToken cancellationToken = default)
    {
        context.Incoterms.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Incoterms.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Incoterms.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SoftDeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Incoterms.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            context.Incoterms.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
