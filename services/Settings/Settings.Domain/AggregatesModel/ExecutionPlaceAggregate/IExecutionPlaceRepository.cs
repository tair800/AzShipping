namespace Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

public interface IExecutionPlaceRepository
{
    Task<ExecutionPlace?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExecutionPlace>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ExecutionPlace> AddAsync(ExecutionPlace entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExecutionPlace entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
