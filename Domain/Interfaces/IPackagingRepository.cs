using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IPackagingRepository
{
    Task<IEnumerable<Packaging>> GetAllAsync();
    Task<Packaging?> GetByIdAsync(Guid id);
    Task<Packaging> CreateAsync(Packaging packaging);
    Task<Packaging> UpdateAsync(Packaging packaging);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

