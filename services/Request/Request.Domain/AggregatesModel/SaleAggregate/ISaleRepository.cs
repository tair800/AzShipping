namespace Request.Domain.AggregatesModel.SaleAggregate;

public interface ISaleRepository
{
    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Sale>> GetAllAsync(string? listStatusFilter = null, CancellationToken cancellationToken = default);
    Task<Sale> AddAsync(Sale entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Sale entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
