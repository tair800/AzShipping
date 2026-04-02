using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class ShippingLineRepository(CarrierDbContext context) : IShippingLineRepository
{
    public async Task<ShippingLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.ShippingLines.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<IReadOnlyList<ShippingLine>> GetAllAsync(bool? isActive, CancellationToken cancellationToken = default)
    {
        var query = context.ShippingLines.AsQueryable();
        if (isActive.HasValue)
            query = query.Where(s => s.IsActive == isActive.Value);
        return await query.OrderBy(s => s.Code ?? s.Name ?? "").ToListAsync(cancellationToken);
    }

    public async Task<ShippingLine> AddAsync(ShippingLine entity, CancellationToken cancellationToken = default)
    {
        await context.ShippingLines.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ShippingLine entity, CancellationToken cancellationToken = default)
    {
        context.ShippingLines.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.ShippingLines.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.ShippingLines.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
