namespace Carrier.Domain.AggregatesModel.AirlineAggregate;

public interface IAirlineRepository
{
    Task<Airline?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Airline>> GetAllAsync(bool? isActive, CancellationToken ct = default);
    Task<Airline> AddAsync(Airline entity, CancellationToken ct = default);
    Task UpdateAsync(Airline entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
