namespace Settings.Domain.AggregatesModel.PackagingAggregate;

public interface IPackagingRepository
{
    Task<Packaging?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Packaging>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Packaging> AddAsync(Packaging entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Packaging entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
