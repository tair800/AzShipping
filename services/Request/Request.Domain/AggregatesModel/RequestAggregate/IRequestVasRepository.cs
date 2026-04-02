namespace Request.Domain.AggregatesModel.RequestAggregate;

public interface IRequestVasRepository
{
    Task<IReadOnlyList<RequestVas>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<RequestVas> entities, CancellationToken cancellationToken = default);
    Task DeleteByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);
}
