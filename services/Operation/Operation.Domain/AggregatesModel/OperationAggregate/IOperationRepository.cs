namespace Operation.Domain.AggregatesModel.OperationAggregate;

public interface IOperationRepository
{
    Task<LogisticsOperation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LogisticsOperation>> GetAllAsync(CancellationToken cancellationToken = default);
    /// <summary>List grid: VasItems + Dimensions only (no package lines).</summary>
    Task<IReadOnlyList<LogisticsOperation>> GetAllForListAsync(CancellationToken cancellationToken = default);
    /// <summary>Trips grid: scalar columns only (no child collections).</summary>
    Task<IReadOnlyList<LogisticsOperation>> GetAllScalarsAsync(CancellationToken cancellationToken = default);
    Task<LogisticsOperation> AddAsync(LogisticsOperation entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(LogisticsOperation entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
