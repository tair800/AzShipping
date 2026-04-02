namespace Carrier.Domain.AggregatesModel.TerminalAggregate;

public interface ITerminalRepository
{
    Task<Terminal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Terminal>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Terminal> AddAsync(Terminal entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Terminal entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
