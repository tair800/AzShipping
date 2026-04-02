using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class PricingTypeRepository(SettingsDbContext context) : IPricingTypeRepository
{
    public async Task<PricingType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.PricingTypes.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<PricingType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.PricingTypes.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<PricingType> AddAsync(PricingType entity, CancellationToken cancellationToken = default)
    {
        context.PricingTypes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(PricingType entity, CancellationToken cancellationToken = default)
    {
        context.PricingTypes.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.PricingTypes.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.PricingTypes.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
