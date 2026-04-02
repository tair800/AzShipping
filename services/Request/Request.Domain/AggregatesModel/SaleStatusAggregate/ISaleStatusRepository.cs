namespace Request.Domain.AggregatesModel.SaleStatusAggregate;

public interface ISaleStatusRepository
{
    Task<SaleStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SaleStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SaleStatus> AddAsync(SaleStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(SaleStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
