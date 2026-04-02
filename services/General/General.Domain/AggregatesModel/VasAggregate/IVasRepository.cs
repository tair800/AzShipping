namespace General.Domain.AggregatesModel.VasAggregate;

public interface IVasRepository
{
    Task<Vas?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Vas>> GetAllAsync(bool? isActive, bool? isDeleted, CancellationToken ct = default);
    Task<Vas> AddAsync(Vas entity, CancellationToken ct = default);
    Task UpdateAsync(Vas entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task SoftDeleteAsync(Guid id, CancellationToken ct = default);
}
