using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IDeferredPaymentConditionService
{
    Task<IEnumerable<DeferredPaymentConditionDto>> GetAllAsync();
    Task<DeferredPaymentConditionDto?> GetByIdAsync(Guid id);
    Task<DeferredPaymentConditionDto> CreateAsync(CreateDeferredPaymentConditionDto createDto);
    Task<DeferredPaymentConditionDto?> UpdateAsync(Guid id, UpdateDeferredPaymentConditionDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

