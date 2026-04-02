namespace Settings.Domain.AggregatesModel.MeetingStatusAggregate;

public interface IMeetingStatusRepository
{
    Task<MeetingStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MeetingStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MeetingStatus> AddAsync(MeetingStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(MeetingStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
