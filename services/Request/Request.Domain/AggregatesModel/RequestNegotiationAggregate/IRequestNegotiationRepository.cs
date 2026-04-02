namespace Request.Domain.AggregatesModel.RequestNegotiationAggregate;

public interface IRequestNegotiationRepository
{
    Task<RequestNegotiation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RequestNegotiation>> GetAllAsync(Guid? clientIdFilter = null, CancellationToken cancellationToken = default);
    Task<RequestNegotiation> AddAsync(RequestNegotiation entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(RequestNegotiation entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
