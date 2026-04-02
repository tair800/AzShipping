namespace Settings.Domain.AggregatesModel.RequestPurposeAggregate;

public interface IRequestPurposeRepository
{
    Task<RequestPurpose?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RequestPurpose>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RequestPurpose> AddAsync(RequestPurpose entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(RequestPurpose entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
