using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.LoadingMethodAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class LoadingMethodRepository(SettingsDbContext context) : ILoadingMethodRepository
{
    public async Task<LoadingMethod?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.LoadingMethods.Include(l => l.Translations).FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

    public async Task<IReadOnlyList<LoadingMethod>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.LoadingMethods.Include(l => l.Translations).OrderBy(l => l.Name).ToListAsync(cancellationToken);

    public async Task<LoadingMethod> AddAsync(LoadingMethod entity, CancellationToken cancellationToken = default)
    {
        context.LoadingMethods.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(LoadingMethod entity, CancellationToken cancellationToken = default)
    {
        context.LoadingMethods.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.LoadingMethods.Include(l => l.Translations).FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        if (e != null) { context.LoadingMethods.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
