using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IDrivingLicenceCategoryRepository
{
    Task<IEnumerable<DrivingLicenceCategory>> GetAllAsync();
    Task<DrivingLicenceCategory?> GetByIdAsync(Guid id);
    Task<DrivingLicenceCategory> CreateAsync(DrivingLicenceCategory category);
    Task<DrivingLicenceCategory> UpdateAsync(DrivingLicenceCategory category);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

