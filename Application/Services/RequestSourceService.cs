using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class RequestSourceService : IRequestSourceService
{
    private readonly IRequestSourceRepository _repository;

    public RequestSourceService(IRequestSourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RequestSourceDto>> GetAllAsync()
    {
        var sources = await _repository.GetAllAsync();
        return sources.Select(MapToDto);
    }

    public async Task<RequestSourceDto?> GetByIdAsync(Guid id)
    {
        var source = await _repository.GetByIdAsync(id);
        return source != null ? MapToDto(source) : null;
    }

    public async Task<RequestSourceDto> CreateAsync(CreateRequestSourceDto createDto)
    {
        var source = new RequestSource
        {
            Id = Guid.NewGuid(),
            Name = createDto.Name,
            IsActive = createDto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(source);
        return MapToDto(created);
    }

    public async Task<RequestSourceDto?> UpdateAsync(Guid id, UpdateRequestSourceDto updateDto)
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

    private static RequestSourceDto MapToDto(RequestSource source)
    {
        return new RequestSourceDto
        {
            Id = source.Id,
            Name = source.Name,
            IsActive = source.IsActive,
            CreatedAt = source.CreatedAt,
            UpdatedAt = source.UpdatedAt
        };
    }
}

