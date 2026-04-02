using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IRequestSourceRepository
{
    Task<IEnumerable<RequestSource>> GetAllAsync();
    Task<RequestSource?> GetByIdAsync(Guid id);
    Task<RequestSource> CreateAsync(RequestSource requestSource);
    Task<RequestSource> UpdateAsync(RequestSource requestSource);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

