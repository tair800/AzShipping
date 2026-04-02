namespace Settings.Domain.AggregatesModel.MeetingPriorityAggregate;

public interface IMeetingPriorityRepository
{
    Task<MeetingPriority?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MeetingPriority>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MeetingPriority> AddAsync(MeetingPriority entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(MeetingPriority entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
