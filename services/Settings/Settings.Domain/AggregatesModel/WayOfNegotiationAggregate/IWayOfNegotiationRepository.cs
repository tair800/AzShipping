namespace Settings.Domain.AggregatesModel.WayOfNegotiationAggregate;

public interface IWayOfNegotiationRepository
{
    Task<WayOfNegotiation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WayOfNegotiation>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WayOfNegotiation> AddAsync(WayOfNegotiation entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(WayOfNegotiation entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
