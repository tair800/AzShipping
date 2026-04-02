using Carrier.Domain.AggregatesModel.TerminalAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class TerminalRepository(CarrierDbContext context) : ITerminalRepository
{
    public async Task<Terminal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Terminals.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Terminal>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Terminals.OrderBy(t => t.Name).ToListAsync(cancellationToken);

    public async Task<Terminal> AddAsync(Terminal entity, CancellationToken cancellationToken = default)
    {
        await context.Terminals.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Terminal entity, CancellationToken cancellationToken = default)
    {
        context.Terminals.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Terminals.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Terminals.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
