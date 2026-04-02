namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public interface ICarrierDocumentRepository
{
    Task<CarrierDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CarrierDocument>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default);
    Task<CarrierDocument> AddAsync(CarrierDocument entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CarrierDocument entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
