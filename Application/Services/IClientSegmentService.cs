using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IClientSegmentService
{
    Task<IEnumerable<ClientSegmentDto>> GetAllAsync();
    Task<ClientSegmentDto?> GetByIdAsync(Guid id);
    Task<ClientSegmentDto> CreateAsync(CreateClientSegmentDto createDto);
    Task<ClientSegmentDto?> UpdateAsync(Guid id, UpdateClientSegmentDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

