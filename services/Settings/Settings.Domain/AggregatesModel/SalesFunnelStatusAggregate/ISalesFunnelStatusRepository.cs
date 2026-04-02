namespace Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;

public interface ISalesFunnelStatusRepository
{
    Task<SalesFunnelStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SalesFunnelStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SalesFunnelStatus> AddAsync(SalesFunnelStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(SalesFunnelStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
