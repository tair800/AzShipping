using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class CarrierTypeRepository(SettingsDbContext context) : ICarrierTypeRepository
{
    public async Task<CarrierType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.CarrierTypes.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<CarrierType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.CarrierTypes.OrderBy(c => c.Name).ToListAsync(cancellationToken);

    public async Task<CarrierType> AddAsync(CarrierType entity, CancellationToken cancellationToken = default)
    {
        context.CarrierTypes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(CarrierType entity, CancellationToken cancellationToken = default)
    {
        context.CarrierTypes.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.CarrierTypes.FindAsync([id], cancellationToken);
        if (e != null) { context.CarrierTypes.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
