using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.WorkerPostAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class WorkerPostRepository(SettingsDbContext context) : IWorkerPostRepository
{
    public async Task<WorkerPost?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.WorkerPosts.Include(w => w.Translations).FirstOrDefaultAsync(w => w.Id == id, cancellationToken);

    public async Task<IReadOnlyList<WorkerPost>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.WorkerPosts.Include(w => w.Translations).OrderBy(w => w.Name).ToListAsync(cancellationToken);

    public async Task<WorkerPost> AddAsync(WorkerPost entity, CancellationToken cancellationToken = default)
    {
        context.WorkerPosts.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(WorkerPost entity, CancellationToken cancellationToken = default)
    {
        context.WorkerPosts.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.WorkerPosts.Include(w => w.Translations).FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
        if (e != null) { context.WorkerPosts.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
