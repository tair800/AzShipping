namespace Carrier.Domain.AggregatesModel.VehicleAggregate;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetAllAsync(CancellationToken ct = default);
    Task<IReadOnlyList<Vehicle>> GetByCarrierIdAsync(Guid carrierId, CancellationToken ct = default);
    Task<Vehicle> AddAsync(Vehicle entity, CancellationToken ct = default);
    Task UpdateAsync(Vehicle entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
