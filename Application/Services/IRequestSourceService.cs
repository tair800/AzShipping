using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IRequestSourceService
{
    Task<IEnumerable<RequestSourceDto>> GetAllAsync();
    Task<RequestSourceDto?> GetByIdAsync(Guid id);
    Task<RequestSourceDto> CreateAsync(CreateRequestSourceDto createDto);
    Task<RequestSourceDto?> UpdateAsync(Guid id, UpdateRequestSourceDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

