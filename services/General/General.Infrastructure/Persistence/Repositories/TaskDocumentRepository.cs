using General.Domain.AggregatesModel.TaskAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class TaskDocumentRepository(GeneralDbContext context) : ITaskDocumentRepository
{
    public async System.Threading.Tasks.Task<TaskDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.TaskDocuments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async System.Threading.Tasks.Task<IReadOnlyList<TaskDocument>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
        => await context.TaskDocuments.Where(d => d.TaskId == taskId).ToListAsync(cancellationToken);

    public async System.Threading.Tasks.Task<TaskDocument> AddAsync(TaskDocument entity, CancellationToken cancellationToken = default)
    {
        context.TaskDocuments.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.TaskDocuments.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.TaskDocuments.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
