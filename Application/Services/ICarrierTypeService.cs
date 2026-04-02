using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface ICarrierTypeService
{
    Task<IEnumerable<CarrierTypeDto>> GetAllAsync();
    Task<CarrierTypeDto?> GetByIdAsync(Guid id);
    Task<CarrierTypeDto> CreateAsync(CreateCarrierTypeDto createDto);
    Task<CarrierTypeDto?> UpdateAsync(Guid id, UpdateCarrierTypeDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

