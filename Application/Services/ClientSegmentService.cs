using AzShipping.Application.DTOs;
using AzShipping.Domain.Entities;
using AzShipping.Domain.Interfaces;

namespace AzShipping.Application.Services;

public class ClientSegmentService : IClientSegmentService
{
    private readonly IClientSegmentRepository _repository;

    public ClientSegmentService(IClientSegmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ClientSegmentDto>> GetAllAsync()
    {
        var segments = await _repository.GetAllAsync();
        return segments.Select(MapToDto);
    }

    public async Task<ClientSegmentDto?> GetByIdAsync(Guid id)
    {
        var segment = await _repository.GetByIdAsync(id);
        return segment != null ? MapToDto(segment) : null;
    }

    public async Task<ClientSegmentDto> CreateAsync(CreateClientSegmentDto createDto)
    {
        var segment = new ClientSegment
        {
            Id = Guid.NewGuid(),
            SegmentName = createDto.SegmentName,
            SegmentPriority = createDto.SegmentPriority,
            IsActive = createDto.IsActive,
            IsDefault = createDto.IsDefault,
            PrimaryColor = createDto.PrimaryColor,
            SecondaryColor = createDto.SecondaryColor,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _repository.CreateAsync(segment);
        return MapToDto(created);
    }

    public async Task<ClientSegmentDto?> UpdateAsync(Guid id, UpdateClientSegmentDto updateDto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return null;

        existing.SegmentName = updateDto.SegmentName;
        existing.SegmentPriority = updateDto.SegmentPriority;
        existing.IsActive = updateDto.IsActive;
        existing.IsDefault = updateDto.IsDefault;
        existing.PrimaryColor = updateDto.PrimaryColor;
        existing.SecondaryColor = updateDto.SecondaryColor;
        existing.UpdatedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static ClientSegmentDto MapToDto(ClientSegment segment)
    {
        return new ClientSegmentDto
        {
            Id = segment.Id,
            SegmentName = segment.SegmentName,
            SegmentPriority = segment.SegmentPriority,
            IsActive = segment.IsActive,
            IsDefault = segment.IsDefault,
            PrimaryColor = segment.PrimaryColor,
            SecondaryColor = segment.SecondaryColor,
            CreatedAt = segment.CreatedAt,
            UpdatedAt = segment.UpdatedAt
        };
    }
}

