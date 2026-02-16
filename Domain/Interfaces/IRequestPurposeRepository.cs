using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IRequestPurposeRepository
{
    Task<IEnumerable<RequestPurpose>> GetAllAsync();
    Task<RequestPurpose?> GetByIdAsync(Guid id);
    Task<RequestPurpose> CreateAsync(RequestPurpose requestPurpose);
    Task<RequestPurpose> UpdateAsync(RequestPurpose requestPurpose);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

