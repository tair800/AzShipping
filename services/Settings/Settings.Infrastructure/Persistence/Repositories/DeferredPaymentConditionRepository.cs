using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.DeferredPaymentConditionAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class DeferredPaymentConditionRepository(SettingsDbContext context) : IDeferredPaymentConditionRepository
{
    public async Task<DeferredPaymentCondition?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.DeferredPaymentConditions.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<DeferredPaymentCondition>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.DeferredPaymentConditions.OrderBy(d => d.Name).ToListAsync(cancellationToken);

    public async Task<DeferredPaymentCondition> AddAsync(DeferredPaymentCondition entity, CancellationToken cancellationToken = default)
    {
        context.DeferredPaymentConditions.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(DeferredPaymentCondition entity, CancellationToken cancellationToken = default)
    {
        context.DeferredPaymentConditions.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.DeferredPaymentConditions.FindAsync([id], cancellationToken);
        if (e != null) { context.DeferredPaymentConditions.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
