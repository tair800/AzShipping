namespace Clients.Domain.AggregatesModel.NegotiationAggregate;

public interface INegotiationRepository
{
    Task<Negotiation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Negotiation>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);
    Task<Negotiation> AddAsync(Negotiation entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Negotiation entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
