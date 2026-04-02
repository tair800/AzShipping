namespace Request.Domain.AggregatesModel.RequestAggregate;

public interface IRequestDimensionRepository
{
    Task<IReadOnlyList<RequestDimension>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<RequestDimension> entities, CancellationToken cancellationToken = default);
    Task DeleteByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);
}
