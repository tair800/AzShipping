namespace Carrier.Domain.AggregatesModel.RailwayStationAggregate;

public interface IRailwayStationRepository
{
    Task<RailwayStation?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<RailwayStation>> GetAllAsync(bool? isActive, CancellationToken ct = default);
    Task<RailwayStation> AddAsync(RailwayStation entity, CancellationToken ct = default);
    Task UpdateAsync(RailwayStation entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
