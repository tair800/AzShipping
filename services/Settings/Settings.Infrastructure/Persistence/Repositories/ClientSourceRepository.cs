using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.ClientSourceAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class ClientSourceRepository(SettingsDbContext context) : IClientSourceRepository
{
    public async Task<ClientSource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.ClientSources.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<ClientSource>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.ClientSources.OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<ClientSource> AddAsync(ClientSource entity, CancellationToken cancellationToken = default)
    {
        await context.ClientSources.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(ClientSource entity, CancellationToken cancellationToken = default)
    {
        context.ClientSources.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.ClientSources.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.ClientSources.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
