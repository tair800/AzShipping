namespace Settings.Domain.AggregatesModel.TaskPriorityAggregate;

public interface ITaskPriorityRepository
{
    Task<TaskPriority?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TaskPriority>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskPriority> AddAsync(TaskPriority entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TaskPriority entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
