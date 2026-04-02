using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.SaleAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class SaleRepository(RequestDbContext context) : ISaleRepository
{
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Sales.Include(s => s.SaleStatus).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Sale>> GetAllAsync(string? listStatusFilter = null, CancellationToken cancellationToken = default)
    {
        var q = context.Sales.Include(s => s.SaleStatus).AsQueryable();
        if (!string.IsNullOrEmpty(listStatusFilter) && !string.Equals(listStatusFilter, "All", StringComparison.OrdinalIgnoreCase))
        {
            q = q.Where(s => s.SaleListStatus == listStatusFilter);
        }
        return await q.OrderByDescending(s => s.CreationDate).ToListAsync(cancellationToken);
    }

    public async Task<Sale> AddAsync(Sale entity, CancellationToken cancellationToken = default)
    {
        context.Sales.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Sale entity, CancellationToken cancellationToken = default)
    {
        context.Sales.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Sales.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Sales.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
