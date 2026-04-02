using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class ShippingAgentRepository(CarrierDbContext context) : IShippingAgentRepository
{
    public async Task<ShippingAgent?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.ShippingAgents.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IReadOnlyList<ShippingAgent>> GetAllAsync(bool? isActive, CancellationToken cancellationToken = default)
    {
        var query = context.ShippingAgents.AsQueryable();
        if (isActive.HasValue)
            query = query.Where(a => a.IsActive == isActive.Value);
        return await query.OrderBy(a => a.CompanyName ?? a.LocalName ?? "").ToListAsync(cancellationToken);
    }

    public async Task<ShippingAgent> AddAsync(ShippingAgent entity, CancellationToken cancellationToken = default)
    {
        await context.ShippingAgents.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ShippingAgent entity, CancellationToken cancellationToken = default)
    {
        context.ShippingAgents.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.ShippingAgents.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.ShippingAgents.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
