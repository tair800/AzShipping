namespace Settings.Domain.AggregatesModel.RequestSourceAggregate;

public interface IRequestSourceRepository
{
    Task<RequestSource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RequestSource>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RequestSource> AddAsync(RequestSource entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(RequestSource entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
