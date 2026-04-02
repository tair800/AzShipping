namespace General.Domain.AggregatesModel.MeetingAggregate;

public interface IMeetingRepository
{
    Task<Meeting?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Meeting>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Meeting> AddAsync(Meeting entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Meeting entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
