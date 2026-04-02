using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class SaleStatusRepository(RequestDbContext context) : ISaleStatusRepository
{
    public async Task<SaleStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.SaleStatuses.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<SaleStatus>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.SaleStatuses.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<SaleStatus> AddAsync(SaleStatus entity, CancellationToken cancellationToken = default)
    {
        context.SaleStatuses.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(SaleStatus entity, CancellationToken cancellationToken = default)
    {
        context.SaleStatuses.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.SaleStatuses.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.SaleStatuses.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
