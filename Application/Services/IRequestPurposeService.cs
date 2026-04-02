using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IRequestPurposeService
{
    Task<IEnumerable<RequestPurposeDto>> GetAllAsync();
    Task<RequestPurposeDto?> GetByIdAsync(Guid id);
    Task<RequestPurposeDto> CreateAsync(CreateRequestPurposeDto createDto);
    Task<RequestPurposeDto?> UpdateAsync(Guid id, UpdateRequestPurposeDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

