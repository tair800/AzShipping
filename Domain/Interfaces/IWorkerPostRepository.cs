using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IWorkerPostRepository
{
    Task<IEnumerable<WorkerPost>> GetAllAsync();
    Task<WorkerPost?> GetByIdAsync(Guid id);
    Task<WorkerPost> CreateAsync(WorkerPost workerPost);
    Task<WorkerPost> UpdateAsync(WorkerPost workerPost);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

