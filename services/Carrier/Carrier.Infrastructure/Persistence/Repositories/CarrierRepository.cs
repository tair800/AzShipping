using Carrier.Domain.AggregatesModel.CarrierAggregate;
using Microsoft.EntityFrameworkCore;
using CarrierEntity = Carrier.Domain.AggregatesModel.CarrierAggregate.Carrier;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class CarrierRepository(CarrierDbContext context) : ICarrierRepository
{
    public async Task<CarrierEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Carriers
            .Include(c => c.ContactPersons)
            .Include(c => c.BankAccounts)
            .Include(c => c.Managers)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IReadOnlyList<CarrierEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Carriers
            .Include(c => c.ContactPersons)
            .Include(c => c.BankAccounts)
            .Include(c => c.Managers)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

    public async Task<CarrierEntity> AddAsync(CarrierEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Carriers.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(CarrierEntity entity, CancellationToken cancellationToken = default)
    {
        context.Carriers.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWithChildrenAsync(CarrierEntity entity, CancellationToken cancellationToken = default)
    {
        var existingContacts = await context.CarrierContactPersons.Where(x => x.CarrierId == entity.Id).ToListAsync(cancellationToken);
        var existingBanks = await context.CarrierBankAccounts.Where(x => x.CarrierId == entity.Id).ToListAsync(cancellationToken);
        var existingManagers = await context.CarrierManagers.Where(x => x.CarrierId == entity.Id).ToListAsync(cancellationToken);
        context.CarrierContactPersons.RemoveRange(existingContacts);
        context.CarrierBankAccounts.RemoveRange(existingBanks);
        context.CarrierManagers.RemoveRange(existingManagers);
        foreach (var c in entity.ContactPersons)
            context.CarrierContactPersons.Add(c);
        foreach (var b in entity.BankAccounts)
            context.CarrierBankAccounts.Add(b);
        foreach (var m in entity.Managers)
            context.CarrierManagers.Add(m);
        context.Carriers.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Carriers.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Carriers.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
