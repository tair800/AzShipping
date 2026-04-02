using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class DrivingLicenceCategoryService : IDrivingLicenceCategoryService
{
    private readonly IDrivingLicenceCategoryRepository _repository;

    public DrivingLicenceCategoryService(IDrivingLicenceCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DrivingLicenceCategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(MapToDto);
    }

    public async Task<DrivingLicenceCategoryDto?> GetByIdAsync(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category != null ? MapToDto(category) : null;
    }

    public async Task<DrivingLicenceCategoryDto> CreateAsync(CreateDrivingLicenceCategoryDto createDto)
    {
        var category = new DrivingLicenceCategory
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(category);
        return MapToDto(created);
    }

    public async Task<DrivingLicenceCategoryDto?> UpdateAsync(Guid id, UpdateDrivingLicenceCategoryDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.Name = updateDto.Name;
        existing.IsActive = updateDto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static DrivingLicenceCategoryDto MapToDto(DrivingLicenceCategory category)
    {
        return new DrivingLicenceCategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            IsActive = category.IsActive,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }
}

