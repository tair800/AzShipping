namespace Settings.Domain.AggregatesModel.StateAggregate;

public interface IStateRepository
{
    Task<State?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<State>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<State>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default);
    Task<State> AddAsync(State entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(State entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
