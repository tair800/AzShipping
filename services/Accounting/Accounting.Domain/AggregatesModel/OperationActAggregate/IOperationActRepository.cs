namespace Accounting.Domain.AggregatesModel.OperationActAggregate;

public interface IOperationActRepository
{
    Task<IReadOnlyList<OperationAct>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OperationAct?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task AddAsync(OperationAct act, CancellationToken cancellationToken = default);
    Task DeleteAsync(OperationAct act, CancellationToken cancellationToken = default);
}
