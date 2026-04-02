using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.BankAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class BankRepository(SettingsDbContext context) : IBankRepository
{
    public async Task<Bank?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Banks
            .Include(b => b.Country)
            .Include(b => b.City)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Bank>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Banks
            .Include(b => b.Country)
            .Include(b => b.City)
            .OrderBy(b => b.Name)
            .ToListAsync(cancellationToken);

    public async Task<Bank> AddAsync(Bank entity, CancellationToken cancellationToken = default)
    {
        context.Banks.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Bank entity, CancellationToken cancellationToken = default)
    {
        context.Banks.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Banks.FindAsync([id], cancellationToken);
        if (e != null) { context.Banks.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
