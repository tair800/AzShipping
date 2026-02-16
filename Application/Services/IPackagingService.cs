using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IPackagingService
{
    Task<IEnumerable<PackagingDto>> GetAllAsync(string? languageCode = null);
    Task<PackagingDto?> GetByIdAsync(Guid id, string? languageCode = null);
    Task<PackagingDto> CreateAsync(CreatePackagingDto createDto);
    Task<PackagingDto?> UpdateAsync(Guid id, UpdatePackagingDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

