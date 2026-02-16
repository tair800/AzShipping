using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface ITransportTypeRepository
{
    Task<IEnumerable<TransportType>> GetAllAsync();
    Task<TransportType?> GetByIdAsync(Guid id);
    Task<TransportType> CreateAsync(TransportType transportType);
    Task<TransportType> UpdateAsync(TransportType transportType);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

