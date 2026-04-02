namespace Operation.Domain.AggregatesModel.OperationAggregate;

public interface IOperationDimensionRepository
{
    Task<IReadOnlyList<OperationDimension>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IReadOnlyList<OperationDimension> items, CancellationToken cancellationToken = default);
    Task DeleteByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
}
