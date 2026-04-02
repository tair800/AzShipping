namespace Operation.Domain.AggregatesModel.OperationAggregate;

public interface IOperationTypeRepository
{
    Task<OperationType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OperationType>> GetAllAsync(bool includeInactive, CancellationToken cancellationToken = default);
}
