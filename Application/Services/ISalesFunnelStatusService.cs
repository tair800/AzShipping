using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface ISalesFunnelStatusService
{
    Task<IEnumerable<SalesFunnelStatusDto>> GetAllAsync(string? languageCode = null);
    Task<SalesFunnelStatusDto?> GetByIdAsync(Guid id, string? languageCode = null);
    Task<SalesFunnelStatusDto> CreateAsync(CreateSalesFunnelStatusDto createDto);
    Task<SalesFunnelStatusDto?> UpdateAsync(Guid id, UpdateSalesFunnelStatusDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

