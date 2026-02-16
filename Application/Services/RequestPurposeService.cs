using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class RequestPurposeService : IRequestPurposeService
{
    private readonly IRequestPurposeRepository _repository;

    public RequestPurposeService(IRequestPurposeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RequestPurposeDto>> GetAllAsync()
    {
        var purposes = await _repository.GetAllAsync();
        return purposes.Select(MapToDto);
    }

    public async Task<RequestPurposeDto?> GetByIdAsync(Guid id)
    {
        var purpose = await _repository.GetByIdAsync(id);
        return purpose != null ? MapToDto(purpose) : null;
    }

    public async Task<RequestPurposeDto> CreateAsync(CreateRequestPurposeDto createDto)
    {
        var purpose = new RequestPurpose
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(purpose);
        return MapToDto(created);
    }

    public async Task<RequestPurposeDto?> UpdateAsync(Guid id, UpdateRequestPurposeDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.Name = updateDto.Name;
        existing.IsActive = updateDto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static RequestPurposeDto MapToDto(RequestPurpose purpose)
    {
        return new RequestPurposeDto
        {
            Id = purpose.Id,
            Name = purpose.Name,
            IsActive = purpose.IsActive,
            CreatedAt = purpose.CreatedAt,
            UpdatedAt = purpose.UpdatedAt
        };
    }
}

