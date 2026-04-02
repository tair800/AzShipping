using Carrier.Domain.AggregatesModel.CarrierAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class TaskRepository(CarrierDbContext context) : ITaskRepository
{
    public async Task<ProjectTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IReadOnlyList<ProjectTask>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default)
        => await context.Tasks
            .Include(t => t.Project)
            .Where(t => t.Project.CarrierId == carrierId)
            .OrderByDescending(t => t.DateOfCreation)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<ProjectTask>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await context.Tasks
            .Where(t => t.ProjectId == projectId)
            .OrderByDescending(t => t.DateOfCreation)
            .ToListAsync(cancellationToken);

    public async Task<int> GetNextTaskSequenceAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var last = await context.Tasks
            .Where(t => t.ProjectId == projectId && t.TaskNo.StartsWith("TASK-"))
            .OrderByDescending(t => t.TaskNo)
            .Select(t => t.TaskNo)
            .FirstOrDefaultAsync(cancellationToken);
        if (string.IsNullOrEmpty(last)) return 1;
        var match = System.Text.RegularExpressions.Regex.Match(last, @"TASK-(\d+)");
        return match.Success && int.TryParse(match.Groups[1].Value, out var n) ? n + 1 : 1;
    }

    public async Task<ProjectTask> AddAsync(ProjectTask entity, CancellationToken cancellationToken = default)
    {
        await context.Tasks.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ProjectTask entity, CancellationToken cancellationToken = default)
    {
        context.Tasks.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Tasks.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Tasks.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
