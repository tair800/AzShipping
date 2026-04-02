namespace Settings.Domain.AggregatesModel.WorkerPostAggregate;

public interface IWorkerPostRepository
{
    Task<WorkerPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<WorkerPost>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WorkerPost> AddAsync(WorkerPost entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(WorkerPost entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
