using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class TaskPriorityRepository(SettingsDbContext context) : ITaskPriorityRepository
{
    public async Task<TaskPriority?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.TaskPriorities.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<TaskPriority>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.TaskPriorities.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<TaskPriority> AddAsync(TaskPriority entity, CancellationToken cancellationToken = default)
    {
        context.TaskPriorities.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TaskPriority entity, CancellationToken cancellationToken = default)
    {
        context.TaskPriorities.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.TaskPriorities.FindAsync([id], cancellationToken);
        if (e != null) { context.TaskPriorities.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
