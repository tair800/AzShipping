namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public interface ICarrierRepository
{
    Task<Carrier?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Carrier>> GetAllAsync(CancellationToken ct = default);
    Task<Carrier> AddAsync(Carrier entity, CancellationToken ct = default);
    Task UpdateAsync(Carrier entity, CancellationToken ct = default);
    Task UpdateWithChildrenAsync(Carrier entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
