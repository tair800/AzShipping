using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class CarrierTypeService : ICarrierTypeService
{
    private readonly ICarrierTypeRepository _repository;

    public CarrierTypeService(ICarrierTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CarrierTypeDto>> GetAllAsync()
    {
        var carrierTypes = await _repository.GetAllAsync();
        return carrierTypes.Select(ct => MapToDto(ct));
    }

    public async Task<CarrierTypeDto?> GetByIdAsync(Guid id)
    {
        var carrierType = await _repository.GetByIdAsync(id);
        return carrierType != null ? MapToDto(carrierType) : null;
    }

    public async Task<CarrierTypeDto> CreateAsync(CreateCarrierTypeDto createDto)
    {
        // If this is set as default, unset other defaults
        if (createDto.IsDefault)
        {
            var existingDefaults = await _repository.GetAllAsync();
            var defaults = existingDefaults.Where(ct => ct.IsDefault).ToList();
            foreach (var defaultType in defaults)
            {
                defaultType.IsDefault = false;
                await _repository.UpdateAsync(defaultType);
            }
        }

        var carrierType = new CarrierType
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            IsActive = createDto.IsActive,
            Colour = createDto.Colour,
            IsDefault = createDto.IsDefault,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(carrierType);
        return MapToDto(created);
    }

    public async Task<CarrierTypeDto?> UpdateAsync(Guid id, UpdateCarrierTypeDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        // If this is set as default, unset other defaults
        if (updateDto.IsDefault && !existing.IsDefault)
        {
            var existingDefaults = await _repository.GetAllAsync();
            var defaults = existingDefaults.Where(ct => ct.IsDefault && ct.Id != id).ToList();
            foreach (var defaultType in defaults)
            {
                defaultType.IsDefault = false;
                await _repository.UpdateAsync(defaultType);
            }
        }

        existing.Name = updateDto.Name;
        existing.IsActive = updateDto.IsActive;
        existing.Colour = updateDto.Colour;
        existing.IsDefault = updateDto.IsDefault;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static CarrierTypeDto MapToDto(CarrierType carrierType)
    {
        return new CarrierTypeDto
        {
            Id = carrierType.Id,
            Name = carrierType.Name,
            IsActive = carrierType.IsActive,
            Colour = carrierType.Colour,
            IsDefault = carrierType.IsDefault,
            CreatedAt = carrierType.CreatedAt,
            UpdatedAt = carrierType.UpdatedAt
        };
    }
}

