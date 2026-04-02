using Carrier.Domain.AggregatesModel.AirlineAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class AirlineRepository(CarrierDbContext context) : IAirlineRepository
{
    public async Task<Airline?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Airlines.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Airline>> GetAllAsync(bool? isActive, CancellationToken cancellationToken = default)
    {
        var query = context.Airlines.AsQueryable();
        if (isActive.HasValue)
            query = query.Where(a => a.IsActive == isActive.Value);
        return await query.OrderBy(a => a.Code ?? a.Name ?? "").ToListAsync(cancellationToken);
    }

    public async Task<Airline> AddAsync(Airline entity, CancellationToken cancellationToken = default)
    {
        await context.Airlines.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Airline entity, CancellationToken cancellationToken = default)
    {
        context.Airlines.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Airlines.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Airlines.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
