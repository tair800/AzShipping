using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface ICarrierTypeRepository
{
    Task<IEnumerable<CarrierType>> GetAllAsync();
    Task<CarrierType?> GetByIdAsync(Guid id);
    Task<CarrierType> CreateAsync(CarrierType carrierType);
    Task<CarrierType> UpdateAsync(CarrierType carrierType);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

