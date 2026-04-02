namespace Carrier.Domain.AggregatesModel.DriverAggregate;

public interface IDriverRepository
{
    Task<Driver?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Driver>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Driver>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default);
    Task<Driver> AddAsync(Driver entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Driver entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
