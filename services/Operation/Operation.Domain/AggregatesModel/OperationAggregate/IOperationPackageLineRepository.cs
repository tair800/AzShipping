namespace Operation.Domain.AggregatesModel.OperationAggregate;

public interface IOperationPackageLineRepository
{
    Task<IReadOnlyList<OperationPackageLine>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IReadOnlyList<OperationPackageLine> items, CancellationToken cancellationToken = default);
    Task DeleteByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
}
