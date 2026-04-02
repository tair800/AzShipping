namespace General.Domain.AggregatesModel.ProjectAggregate;

public interface IProjectRepository
{
    System.Threading.Tasks.Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<Project> AddAsync(Project entity, CancellationToken cancellationToken = default);
}
