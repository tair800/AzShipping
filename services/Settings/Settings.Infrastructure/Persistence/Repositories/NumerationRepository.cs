using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Settings.Domain.AggregatesModel.NumerationAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class NumerationRepository(SettingsDbContext context) : INumerationRepository
{
    public async Task<IReadOnlyList<Numeration>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Numerations
            .Include(n => n.Company)
            .Include(n => n.Department)
            .OrderBy(n => n.Name)
            .ToListAsync(cancellationToken);

    public async Task<Numeration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Numerations
            .Include(n => n.Company)
            .Include(n => n.Department)
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Numeration>> GetCandidatesAsync(string numerationForCode,
        CancellationToken cancellationToken = default)
        => await context.Numerations
            .Where(n => n.NumerationForCode == numerationForCode)
            .OrderBy(n => n.Name)
            .ToListAsync(cancellationToken);

    public async Task<int?> IncrementIndexAtomicallyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        for (var attempt = 1; attempt <= 5; attempt++)
        {
            await using var tx = await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
            try
            {
                var row = await context.Numerations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
                if (row == null)
                {
                    await tx.RollbackAsync(cancellationToken);
                    return null;
                }

                row.CurrentIndex += 1;
                row.UpdatedAt = DateTime.UtcNow;
                await context.SaveChangesAsync(cancellationToken);
                await tx.CommitAsync(cancellationToken);
                return row.CurrentIndex;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException p &&
                                               (p.SqlState == PostgresErrorCodes.SerializationFailure ||
                                                p.SqlState == PostgresErrorCodes.DeadlockDetected))
            {
                await tx.RollbackAsync(cancellationToken);
                if (attempt == 5) throw;
                await Task.Delay(10 * attempt, cancellationToken);
            }
        }

        return null;
    }

    public async Task AddAsync(Numeration entity, CancellationToken cancellationToken = default)
    {
        context.Numerations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Numeration entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        context.Numerations.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Numerations.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Numerations.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
