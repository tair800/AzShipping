namespace General.Domain.AggregatesModel.TaskAggregate;

public interface ITaskDocumentRepository
{
    System.Threading.Tasks.Task<TaskDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<TaskDocument>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<TaskDocument> AddAsync(TaskDocument entity, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
