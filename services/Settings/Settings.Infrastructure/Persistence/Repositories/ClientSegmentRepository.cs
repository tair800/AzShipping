using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class ClientSegmentRepository(SettingsDbContext context) : IClientSegmentRepository
{
    public async Task<ClientSegment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.ClientSegments.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<ClientSegment>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.ClientSegments.OrderBy(c => c.SegmentPriority).ToListAsync(cancellationToken);

    public async Task<ClientSegment> AddAsync(ClientSegment entity, CancellationToken cancellationToken = default)
    {
        context.ClientSegments.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ClientSegment entity, CancellationToken cancellationToken = default)
    {
        context.ClientSegments.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.ClientSegments.FindAsync([id], cancellationToken);
        if (e != null) { context.ClientSegments.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
