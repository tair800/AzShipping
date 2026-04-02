namespace Operation.Domain.AggregatesModel.OperationAggregate;

public interface IOperationVasRepository
{
    Task<IReadOnlyList<OperationVas>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IReadOnlyList<OperationVas> items, CancellationToken cancellationToken = default);
    Task DeleteByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
}
