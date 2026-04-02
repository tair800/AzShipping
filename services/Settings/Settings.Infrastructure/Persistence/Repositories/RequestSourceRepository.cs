using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class RequestSourceRepository(SettingsDbContext context) : IRequestSourceRepository
{
    public async Task<RequestSource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.RequestSources.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<RequestSource>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.RequestSources.OrderBy(r => r.Name).ToListAsync(cancellationToken);

    public async Task<RequestSource> AddAsync(RequestSource entity, CancellationToken cancellationToken = default)
    {
        context.RequestSources.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(RequestSource entity, CancellationToken cancellationToken = default)
    {
        context.RequestSources.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.RequestSources.FindAsync([id], cancellationToken);
        if (e != null) { context.RequestSources.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
