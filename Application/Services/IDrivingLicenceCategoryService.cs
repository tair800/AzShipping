using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IDrivingLicenceCategoryService
{
    Task<IEnumerable<DrivingLicenceCategoryDto>> GetAllAsync();
    Task<DrivingLicenceCategoryDto?> GetByIdAsync(Guid id);
    Task<DrivingLicenceCategoryDto> CreateAsync(CreateDrivingLicenceCategoryDto createDto);
    Task<DrivingLicenceCategoryDto?> UpdateAsync(Guid id, UpdateDrivingLicenceCategoryDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

