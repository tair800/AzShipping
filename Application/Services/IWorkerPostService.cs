using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IWorkerPostService
{
    Task<IEnumerable<WorkerPostDto>> GetAllAsync(string? languageCode = null);
    Task<WorkerPostDto?> GetByIdAsync(Guid id, string? languageCode = null);
    Task<WorkerPostDto> CreateAsync(CreateWorkerPostDto createDto);
    Task<WorkerPostDto?> UpdateAsync(Guid id, UpdateWorkerPostDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}

