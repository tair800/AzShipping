using Clients.Domain.AggregatesModel.DirectionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence.Repositories;

public class DirectionRepository(ClientsDbContext context) : IDirectionRepository
{
    public async Task<Direction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Directions.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Direction>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default)
        => await context.Directions
            .Where(d => d.ClientId == clientId)
            .ToListAsync(cancellationToken);

    public async Task<Direction> AddAsync(Direction entity, CancellationToken cancellationToken = default)
    {
        context.Directions.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Direction entity, CancellationToken cancellationToken = default)
    {
        context.Directions.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Directions.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Directions.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
