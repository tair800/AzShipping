using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.DrivingLicenceCategoryAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class DrivingLicenceCategoryRepository(SettingsDbContext context) : IDrivingLicenceCategoryRepository
{
    public async Task<DrivingLicenceCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.DrivingLicenceCategories.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<DrivingLicenceCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.DrivingLicenceCategories.OrderBy(d => d.Name).ToListAsync(cancellationToken);

    public async Task<DrivingLicenceCategory> AddAsync(DrivingLicenceCategory entity, CancellationToken cancellationToken = default)
    {
        context.DrivingLicenceCategories.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(DrivingLicenceCategory entity, CancellationToken cancellationToken = default)
    {
        context.DrivingLicenceCategories.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.DrivingLicenceCategories.FindAsync([id], cancellationToken);
        if (e != null) { context.DrivingLicenceCategories.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
