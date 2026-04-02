using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.CountryAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class CountryRepository(SettingsDbContext context) : ICountryRepository
{
    public async Task<Country?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Countries
            .Include(x => x.CountryGlobalZones)
                .ThenInclude(x => x.GlobalZone)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Countries
            .Include(x => x.CountryGlobalZones)
                .ThenInclude(x => x.GlobalZone)
            .Where(x => x.Status != EntityStatus.Deleted)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Country>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default)
    {
        return await context.Countries
            .Include(x => x.CountryGlobalZones)
                .ThenInclude(x => x.GlobalZone)
            .Where(x => x.Status == status)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Country> AddAsync(Country entity, CancellationToken cancellationToken = default)
    {
        await context.Countries.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Country entity, CancellationToken cancellationToken = default)
    {
        context.Countries.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Countries.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            entity.Status = EntityStatus.Deleted;
            entity.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SetGlobalZonesAsync(Guid countryId, IEnumerable<Guid> globalZoneIds, CancellationToken cancellationToken = default)
    {
        var existing = await context.CountryGlobalZones
            .Where(x => x.CountryId == countryId)
            .ToListAsync(cancellationToken);
        
        context.CountryGlobalZones.RemoveRange(existing);
        
        var newEntries = globalZoneIds.Select(gzId => new CountryGlobalZone
        {
            CountryId = countryId,
            GlobalZoneId = gzId
        });
        
        await context.CountryGlobalZones.AddRangeAsync(newEntries, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
