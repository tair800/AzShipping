using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class RailwayStationRepository(CarrierDbContext context) : IRailwayStationRepository
{
    public async Task<RailwayStation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.RailwayStations.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<IReadOnlyList<RailwayStation>> GetAllAsync(bool? isActive, CancellationToken cancellationToken = default)
    {
        var query = context.RailwayStations.AsQueryable();
        if (isActive.HasValue)
            query = query.Where(r => r.IsActive == isActive.Value);
        return await query.OrderBy(r => r.Code ?? r.Name ?? "").ToListAsync(cancellationToken);
    }

    public async Task<RailwayStation> AddAsync(RailwayStation entity, CancellationToken cancellationToken = default)
    {
        await context.RailwayStations.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(RailwayStation entity, CancellationToken cancellationToken = default)
    {
        context.RailwayStations.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.RailwayStations.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.RailwayStations.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
