namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public interface ITaskRepository
{
    System.Threading.Tasks.Task<ProjectTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<ProjectTask>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<int> GetNextTaskSequenceAsync(Guid projectId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<ProjectTask> AddAsync(ProjectTask entity, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task UpdateAsync(ProjectTask entity, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
