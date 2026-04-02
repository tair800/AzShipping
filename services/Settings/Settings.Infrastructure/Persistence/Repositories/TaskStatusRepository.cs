using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.TaskStatusAggregate;
using TaskStatusEntity = Settings.Domain.AggregatesModel.TaskStatusAggregate.TaskStatus;

namespace Settings.Infrastructure.Persistence.Repositories;

public class TaskStatusRepository(SettingsDbContext context) : ITaskStatusRepository
{
    public async Task<TaskStatusEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.TaskStatuses.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<TaskStatusEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.TaskStatuses.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<TaskStatusEntity> AddAsync(TaskStatusEntity entity, CancellationToken cancellationToken = default)
    {
        context.TaskStatuses.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(TaskStatusEntity entity, CancellationToken cancellationToken = default)
    {
        context.TaskStatuses.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.TaskStatuses.FindAsync([id], cancellationToken);
        if (e != null) { context.TaskStatuses.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
