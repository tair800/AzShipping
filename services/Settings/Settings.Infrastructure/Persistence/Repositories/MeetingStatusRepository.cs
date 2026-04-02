using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class MeetingStatusRepository(SettingsDbContext context) : IMeetingStatusRepository
{
    public async Task<MeetingStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.MeetingStatuses.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<MeetingStatus>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.MeetingStatuses.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<MeetingStatus> AddAsync(MeetingStatus entity, CancellationToken cancellationToken = default)
    {
        context.MeetingStatuses.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(MeetingStatus entity, CancellationToken cancellationToken = default)
    {
        context.MeetingStatuses.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.MeetingStatuses.FindAsync([id], cancellationToken);
        if (e != null) { context.MeetingStatuses.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
