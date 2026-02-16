using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface ISalesFunnelStatusRepository
{
    Task<IEnumerable<SalesFunnelStatus>> GetAllAsync();
    Task<SalesFunnelStatus?> GetByIdAsync(Guid id);
    Task<SalesFunnelStatus> CreateAsync(SalesFunnelStatus status);
    Task<SalesFunnelStatus> UpdateAsync(SalesFunnelStatus status);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

