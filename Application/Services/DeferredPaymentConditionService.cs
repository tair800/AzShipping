using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class DeferredPaymentConditionService : IDeferredPaymentConditionService
{
    private readonly IDeferredPaymentConditionRepository _repository;

    public DeferredPaymentConditionService(IDeferredPaymentConditionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DeferredPaymentConditionDto>> GetAllAsync()
    {
        var conditions = await _repository.GetAllAsync();
        return conditions.Select(c => MapToDto(c));
    }

    public async Task<DeferredPaymentConditionDto?> GetByIdAsync(Guid id)
    {
        var condition = await _repository.GetByIdAsync(id);
        return condition != null ? MapToDto(condition) : null;
    }

    public async Task<DeferredPaymentConditionDto> CreateAsync(CreateDeferredPaymentConditionDto createDto)
    {
        var condition = new DeferredPaymentCondition
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            FullText = createDto.FullText,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(condition);
        return MapToDto(created);
    }

    public async Task<DeferredPaymentConditionDto?> UpdateAsync(Guid id, UpdateDeferredPaymentConditionDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.Name = updateDto.Name;
        existing.FullText = updateDto.FullText;
        existing.IsActive = updateDto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static DeferredPaymentConditionDto MapToDto(DeferredPaymentCondition condition)
    {
        return new DeferredPaymentConditionDto
        {
            Id = condition.Id,
            Name = condition.Name,
            FullText = condition.FullText,
            IsActive = condition.IsActive,
            CreatedAt = condition.CreatedAt,
            UpdatedAt = condition.UpdatedAt
        };
    }
}

