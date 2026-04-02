using General.Domain.AggregatesModel.ProjectAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class ProjectRepository(GeneralDbContext context) : IProjectRepository
{
    public async System.Threading.Tasks.Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async System.Threading.Tasks.Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Projects.OrderBy(p => p.Name).ToListAsync(cancellationToken);

    public async System.Threading.Tasks.Task<Project> AddAsync(Project entity, CancellationToken cancellationToken = default)
    {
        context.Projects.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
