using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface ILoadingMethodService
{
    Task<IEnumerable<LoadingMethodDto>> GetAllAsync(string? languageCode = null);
    Task<LoadingMethodDto?> GetByIdAsync(Guid id, string? languageCode = null);
    Task<LoadingMethodDto> CreateAsync(CreateLoadingMethodDto createDto);
    Task<LoadingMethodDto?> UpdateAsync(Guid id, UpdateLoadingMethodDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

