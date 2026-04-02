namespace General.Domain.AggregatesModel.TaskAggregate;

public interface ITaskRepository
{
    System.Threading.Tasks.Task<GeneralTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<GeneralTask>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Tasks tied to an operation: <see cref="GeneralTask.OperationId"/> or Operations module <see cref="GeneralTask.RelatedRecordId"/>.</summary>
    System.Threading.Tasks.Task<IReadOnlyList<GeneralTask>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<int> GetNextTaskSequenceAsync(CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<GeneralTask> AddAsync(GeneralTask entity, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task UpdateAsync(GeneralTask entity, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>Tasks assigned to <paramref name="responsibleUserId"/> with DateOfCreation in [startUtcInclusive, endUtcExclusive).</summary>
    System.Threading.Tasks.Task<IReadOnlyList<GeneralTask>> GetByResponsibleUserIdCreatedInRangeAsync(
        long responsibleUserId,
        DateTime startUtcInclusive,
        DateTime endUtcExclusive,
        CancellationToken cancellationToken = default);
}
