namespace Settings.Domain.AggregatesModel.MeetingResultAggregate;

public interface IMeetingResultRepository
{
    Task<MeetingResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MeetingResult>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MeetingResult> AddAsync(MeetingResult entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(MeetingResult entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
