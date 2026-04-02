namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public interface ICarrierDirectionRepository
{
    Task<CarrierDirection?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CarrierDirection>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default);
    Task<CarrierDirection> AddAsync(CarrierDirection entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CarrierDirection entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
