using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Domain.AggregatesModel.GlobalZoneAggregate;

public interface IGlobalZoneRepository
{
    Task<GlobalZone?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GlobalZone>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GlobalZone>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default);
    Task<GlobalZone> AddAsync(GlobalZone entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(GlobalZone entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
