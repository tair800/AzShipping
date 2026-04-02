using General.Domain.AggregatesModel.MeetingHistoryAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class MeetingHistoryRepository(GeneralDbContext context) : IMeetingHistoryRepository
{
    public async Task<MeetingHistory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.MeetingHistories.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<MeetingHistory>> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default)
        => await context.MeetingHistories
            .Where(x => x.MeetingId == meetingId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

    public async Task<MeetingHistory> AddAsync(MeetingHistory entity, CancellationToken cancellationToken = default)
    {
        await context.MeetingHistories.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
