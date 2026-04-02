namespace Settings.Domain.AggregatesModel.MeetingTypeAggregate;

public interface IMeetingTypeRepository
{
    Task<MeetingType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MeetingType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MeetingType> AddAsync(MeetingType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(MeetingType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
