using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.CityAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class CityRepository(SettingsDbContext context) : ICityRepository
{
    public async Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Cities.Include(x => x.State).ThenInclude(s => s!.Country).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<City>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Cities.Include(x => x.State).ThenInclude(s => s!.Country).Where(x => x.Status != EntityStatus.Deleted).OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<City>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default)
        => await context.Cities.Include(x => x.State).ThenInclude(s => s!.Country).Where(x => x.Status == status).OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<City> AddAsync(City entity, CancellationToken cancellationToken = default)
    {
        await context.Cities.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(City entity, CancellationToken cancellationToken = default)
    {
        context.Cities.Update(entity);
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
