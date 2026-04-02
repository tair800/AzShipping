using Accounting.Domain.AggregatesModel.OperationActAggregate;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence.Repositories;

public sealed class OperationActRepository(AccountingDbContext db) : IOperationActRepository
{
    public async Task<IReadOnlyList<OperationAct>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await db.OperationActs
            .AsNoTracking()
            .OrderByDescending(a => a.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<OperationAct?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await db.OperationActs.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task AddAsync(OperationAct act, CancellationToken cancellationToken = default)
    {
        await db.OperationActs.AddAsync(act, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(OperationAct act, CancellationToken cancellationToken = default)
    {
        db.OperationActs.Remove(act);
        await db.SaveChangesAsync(cancellationToken);
    }
}
