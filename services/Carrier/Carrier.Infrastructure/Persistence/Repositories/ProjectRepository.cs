using Carrier.Domain.AggregatesModel.CarrierAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class ProjectRepository(CarrierDbContext context) : IProjectRepository
{
    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Project>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default)
        => await context.Projects
            .Where(p => p.CarrierId == carrierId)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

    public async Task<Project> AddAsync(Project entity, CancellationToken cancellationToken = default)
    {
        await context.Projects.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
