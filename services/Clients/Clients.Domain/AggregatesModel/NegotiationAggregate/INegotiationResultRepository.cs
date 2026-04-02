namespace Clients.Domain.AggregatesModel.NegotiationAggregate;

public interface INegotiationResultRepository
{
    Task<IReadOnlyList<NegotiationResult>> GetByNegotiationIdAsync(Guid negotiationId, CancellationToken cancellationToken = default);
    Task<NegotiationResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<NegotiationResult> AddAsync(NegotiationResult entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(NegotiationResult entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteByNegotiationIdAsync(Guid negotiationId, CancellationToken cancellationToken = default);
}
