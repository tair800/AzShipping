using Clients.Domain.AggregatesModel.ClientAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence.Repositories;

public class ClientRepository(ClientsDbContext context) : IClientRepository
{
    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Clients
            .Include(c => c.ContactPersons)
            .Include(c => c.BankAccounts)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Clients
            .Include(c => c.ContactPersons)
            .Include(c => c.BankAccounts)
            .OrderBy(c => c.CompanyName)
            .ToListAsync(cancellationToken);

    public async Task<Client> AddAsync(Client entity, CancellationToken cancellationToken = default)
    {
        context.Clients.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Client entity, CancellationToken cancellationToken = default)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Clients.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Clients.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
