namespace General.Domain.AggregatesModel.VesselAggregate;

public interface IVesselRepository
{
    Task<Vessel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Vessel>> GetAllAsync(bool? isActive, bool? isDeleted, CancellationToken ct = default);
    Task<Vessel> AddAsync(Vessel entity, CancellationToken ct = default);
    Task UpdateAsync(Vessel entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task SoftDeleteAsync(Guid id, CancellationToken ct = default);
}
