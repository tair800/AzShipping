using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class ExecutionPlaceRepository(SettingsDbContext context) : IExecutionPlaceRepository
{
    public async Task<ExecutionPlace?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.ExecutionPlaces.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<ExecutionPlace>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.ExecutionPlaces.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<ExecutionPlace> AddAsync(ExecutionPlace entity, CancellationToken cancellationToken = default)
    {
        context.ExecutionPlaces.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ExecutionPlace entity, CancellationToken cancellationToken = default)
    {
        context.ExecutionPlaces.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.ExecutionPlaces.FindAsync([id], cancellationToken);
        if (e != null) { context.ExecutionPlaces.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
