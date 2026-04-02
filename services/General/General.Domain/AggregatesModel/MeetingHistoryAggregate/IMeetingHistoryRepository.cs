namespace General.Domain.AggregatesModel.MeetingHistoryAggregate;

public interface IMeetingHistoryRepository
{
    Task<MeetingHistory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MeetingHistory>> GetByMeetingIdAsync(Guid meetingId, CancellationToken cancellationToken = default);
    Task<MeetingHistory> AddAsync(MeetingHistory entity, CancellationToken cancellationToken = default);
}
