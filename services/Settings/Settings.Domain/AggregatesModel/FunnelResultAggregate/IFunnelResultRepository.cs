namespace Settings.Domain.AggregatesModel.FunnelResultAggregate;

public interface IFunnelResultRepository
{
    Task<FunnelResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FunnelResult>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FunnelResult> AddAsync(FunnelResult entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(FunnelResult entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
