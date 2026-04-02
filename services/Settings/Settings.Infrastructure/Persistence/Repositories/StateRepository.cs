using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class StateRepository(SettingsDbContext context) : IStateRepository
{
    public async Task<State?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.States.Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<State>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.States.Include(x => x.Country).Where(x => x.Status != EntityStatus.Deleted).OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<State>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default)
        => await context.States.Include(x => x.Country).Where(x => x.Status == status).OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<State> AddAsync(State entity, CancellationToken cancellationToken = default)
    {
        await context.States.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(State entity, CancellationToken cancellationToken = default)
    {
        context.States.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            entity.Status = EntityStatus.Deleted;
            entity.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
