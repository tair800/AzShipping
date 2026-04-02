namespace Settings.Domain.AggregatesModel.LoadingMethodAggregate;

public interface ILoadingMethodRepository
{
    Task<LoadingMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LoadingMethod>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LoadingMethod> AddAsync(LoadingMethod entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(LoadingMethod entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
