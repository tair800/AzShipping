using Carrier.Domain.AggregatesModel.VehicleAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class VehicleRepository(CarrierDbContext context) : IVehicleRepository
{
    public async Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Vehicles.FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Vehicles.OrderBy(v => v.VehicleNumber).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Vehicle>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default)
        => await context.Vehicles
            .Where(v => v.CarrierId == carrierId)
            .OrderBy(v => v.VehicleNumber)
            .ToListAsync(cancellationToken);

    public async Task<Vehicle> AddAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        await context.Vehicles.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Vehicle entity, CancellationToken cancellationToken = default)
    {
        context.Vehicles.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Vehicles.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Vehicles.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
