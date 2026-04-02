namespace Settings.Domain.AggregatesModel.NumerationAggregate;

public interface INumerationRepository
{
    Task<IReadOnlyList<Numeration>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Numeration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Numeration>> GetCandidatesAsync(string numerationForCode, CancellationToken cancellationToken = default);
    Task<int?> IncrementIndexAtomicallyAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Numeration entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Numeration entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
