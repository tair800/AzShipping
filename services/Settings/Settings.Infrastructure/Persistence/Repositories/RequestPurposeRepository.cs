using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.RequestPurposeAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class RequestPurposeRepository(SettingsDbContext context) : IRequestPurposeRepository
{
    public async Task<RequestPurpose?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.RequestPurposes.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<RequestPurpose>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.RequestPurposes.OrderBy(r => r.Name).ToListAsync(cancellationToken);

    public async Task<RequestPurpose> AddAsync(RequestPurpose entity, CancellationToken cancellationToken = default)
    {
        context.RequestPurposes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(RequestPurpose entity, CancellationToken cancellationToken = default)
    {
        context.RequestPurposes.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.RequestPurposes.FindAsync([id], cancellationToken);
        if (e != null) { context.RequestPurposes.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
