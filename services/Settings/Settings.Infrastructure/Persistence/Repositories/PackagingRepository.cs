using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.PackagingAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class PackagingRepository(SettingsDbContext context) : IPackagingRepository
{
    public async Task<Packaging?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Packagings.Include(p => p.Translations).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Packaging>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Packagings.Include(p => p.Translations).OrderBy(p => p.Name).ToListAsync(cancellationToken);

    public async Task<Packaging> AddAsync(Packaging entity, CancellationToken cancellationToken = default)
    {
        context.Packagings.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Packaging entity, CancellationToken cancellationToken = default)
    {
        context.Packagings.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Packagings.Include(p => p.Translations).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (e != null) { context.Packagings.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
