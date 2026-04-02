using General.Domain.AggregatesModel.TaskAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class TaskRepository(GeneralDbContext context) : ITaskRepository
{
    public async System.Threading.Tasks.Task<GeneralTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Tasks
            .Include(t => t.Project)
            .Include(t => t.Documents)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async System.Threading.Tasks.Task<IReadOnlyList<GeneralTask>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Tasks
            .Include(t => t.Project)
            .Include(t => t.Documents)
            .OrderByDescending(t => t.DateOfCreation)
            .ToListAsync(cancellationToken);

    public async System.Threading.Tasks.Task<IReadOnlyList<GeneralTask>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
        => await context.Tasks
            .Include(t => t.Project)
            .Include(t => t.Documents)
            .Where(t => t.OperationId == operationId
                        || (t.RelatedModule == TaskRelatedModule.Operations && t.RelatedRecordId == operationId))
            .OrderByDescending(t => t.DateOfCreation)
            .ToListAsync(cancellationToken);

    public async System.Threading.Tasks.Task<int> GetNextTaskSequenceAsync(CancellationToken cancellationToken = default)
    {
        var all = await context.Tasks.Select(t => t.TaskNo).ToListAsync(cancellationToken);
        var max = 0;
        foreach (var no in all)
        {
            if (!string.IsNullOrEmpty(no) && int.TryParse(no, out var n) && n > max) max = n;
        }
        return max + 1;
    }

    public async System.Threading.Tasks.Task<GeneralTask> AddAsync(GeneralTask entity, CancellationToken cancellationToken = default)
    {
        context.Tasks.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(GeneralTask entity, CancellationToken cancellationToken = default)
    {
        context.Tasks.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Tasks.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Tasks.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async System.Threading.Tasks.Task<IReadOnlyList<GeneralTask>> GetByResponsibleUserIdCreatedInRangeAsync(
        long responsibleUserId,
        DateTime startUtcInclusive,
        DateTime endUtcExclusive,
        CancellationToken cancellationToken = default)
        => await context.Tasks.AsNoTracking()
            .Where(t => t.ResponsibleUserId == responsibleUserId
                        && t.DateOfCreation >= startUtcInclusive
                        && t.DateOfCreation < endUtcExclusive)
            .ToListAsync(cancellationToken);
}
