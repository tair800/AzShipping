namespace Clients.Domain.AggregatesModel.DirectionAggregate;

public interface IDirectionRepository
{
    Task<Direction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Direction>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);
    Task<Direction> AddAsync(Direction entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Direction entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
