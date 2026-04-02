using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface ILoadingMethodRepository
{
    Task<IEnumerable<LoadingMethod>> GetAllAsync();
    Task<LoadingMethod?> GetByIdAsync(Guid id);
    Task<LoadingMethod> CreateAsync(LoadingMethod loadingMethod);
    Task<LoadingMethod> UpdateAsync(LoadingMethod loadingMethod);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

