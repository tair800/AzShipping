using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IDeferredPaymentConditionRepository
{
    Task<IEnumerable<DeferredPaymentCondition>> GetAllAsync();
    Task<DeferredPaymentCondition?> GetByIdAsync(Guid id);
    Task<DeferredPaymentCondition> CreateAsync(DeferredPaymentCondition condition);
    Task<DeferredPaymentCondition> UpdateAsync(DeferredPaymentCondition condition);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

