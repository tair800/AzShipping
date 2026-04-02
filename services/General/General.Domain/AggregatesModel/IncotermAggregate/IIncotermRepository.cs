namespace General.Domain.AggregatesModel.IncotermAggregate;

public interface IIncotermRepository
{
    Task<Incoterm?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Incoterm>> GetAllAsync(bool? isActive, bool? isDeleted, CancellationToken ct = default);
    Task<Incoterm> AddAsync(Incoterm entity, CancellationToken ct = default);
    Task UpdateAsync(Incoterm entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task SoftDeleteAsync(Guid id, CancellationToken ct = default);
}
