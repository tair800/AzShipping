namespace Settings.Domain.AggregatesModel.TaskStatusAggregate;

public interface ITaskStatusRepository
{
    Task<TaskStatus?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TaskStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskStatus> AddAsync(TaskStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
