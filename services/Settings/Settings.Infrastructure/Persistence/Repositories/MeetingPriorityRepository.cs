using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class MeetingPriorityRepository(SettingsDbContext context) : IMeetingPriorityRepository
{
    public async Task<MeetingPriority?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.MeetingPriorities.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<MeetingPriority>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.MeetingPriorities.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<MeetingPriority> AddAsync(MeetingPriority entity, CancellationToken cancellationToken = default)
    {
        context.MeetingPriorities.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(MeetingPriority entity, CancellationToken cancellationToken = default)
    {
        context.MeetingPriorities.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.MeetingPriorities.FindAsync([id], cancellationToken);
        if (e != null) { context.MeetingPriorities.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
