using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class MeetingResultRepository(SettingsDbContext context) : IMeetingResultRepository
{
    public async Task<MeetingResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.MeetingResults.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<MeetingResult>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.MeetingResults.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<MeetingResult> AddAsync(MeetingResult entity, CancellationToken cancellationToken = default)
    {
        context.MeetingResults.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(MeetingResult entity, CancellationToken cancellationToken = default)
    {
        context.MeetingResults.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.MeetingResults.FindAsync([id], cancellationToken);
        if (e != null) { context.MeetingResults.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
