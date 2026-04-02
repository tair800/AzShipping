using Carrier.Domain.AggregatesModel.CarrierAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class CarrierDirectionRepository(CarrierDbContext context) : ICarrierDirectionRepository
{
    public async Task<CarrierDirection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.CarrierDirections
            .Include(d => d.TransportTypes)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<IReadOnlyList<CarrierDirection>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default)
        => await context.CarrierDirections
            .Include(d => d.TransportTypes)
            .Where(d => d.CarrierId == carrierId)
            .OrderBy(d => d.Id)
            .ToListAsync(cancellationToken);

    public async Task<CarrierDirection> AddAsync(CarrierDirection entity, CancellationToken cancellationToken = default)
    {
        await context.CarrierDirections.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(CarrierDirection entity, CancellationToken cancellationToken = default)
    {
        context.CarrierDirections.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.CarrierDirections.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.CarrierDirections.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
