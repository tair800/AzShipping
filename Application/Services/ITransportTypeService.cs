using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface ITransportTypeService
{
    Task<IEnumerable<TransportTypeDto>> GetAllAsync(string? languageCode = null);
    Task<TransportTypeDto?> GetByIdAsync(Guid id, string? languageCode = null);
    Task<TransportTypeDto> CreateAsync(CreateTransportTypeDto createDto);
    Task<TransportTypeDto?> UpdateAsync(Guid id, UpdateTransportTypeDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

