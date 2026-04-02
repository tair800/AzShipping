namespace Settings.Domain.AggregatesModel.CarrierTypeAggregate;

public interface ICarrierTypeRepository
{
    Task<CarrierType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CarrierType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CarrierType> AddAsync(CarrierType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CarrierType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
