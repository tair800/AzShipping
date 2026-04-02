using Carrier.Domain.AggregatesModel.DriverAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class DriverRepository(CarrierDbContext context) : IDriverRepository
{
    public async Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Drivers
            .Include(d => d.DriverCarriers)
            .Include(d => d.DrivingLicenceCategories)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Driver>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Drivers
            .Include(d => d.DriverCarriers)
            .Include(d => d.DrivingLicenceCategories)
            .OrderBy(d => d.Surname).ThenBy(d => d.Name)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Driver>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default)
        => await context.Drivers
            .Include(d => d.DriverCarriers)
            .Include(d => d.DrivingLicenceCategories)
            .Where(d => d.DriverCarriers.Any(dc => dc.CarrierId == carrierId))
            .OrderBy(d => d.Surname).ThenBy(d => d.Name)
            .ToListAsync(cancellationToken);

    public async Task<Driver> AddAsync(Driver entity, CancellationToken cancellationToken = default)
    {
        await context.Drivers.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Driver entity, CancellationToken cancellationToken = default)
    {
        context.Drivers.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Drivers.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Drivers.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
