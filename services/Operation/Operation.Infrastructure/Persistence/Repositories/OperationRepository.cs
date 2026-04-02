using Microsoft.EntityFrameworkCore;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Infrastructure.Persistence.Repositories;

public class OperationRepository(OperationDbContext context) : IOperationRepository
{
    public async Task<LogisticsOperation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Operations
            .Include(x => x.Dimensions)
            .Include(x => x.PackageLines)
            .Include(x => x.VasItems)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<LogisticsOperation>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Operations
            .AsNoTracking()
            .Include(x => x.Dimensions)
            .Include(x => x.PackageLines)
            .Include(x => x.VasItems)
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<LogisticsOperation>> GetAllForListAsync(CancellationToken cancellationToken = default)
        => await context.Operations
            .AsNoTracking()
            .Include(x => x.Dimensions)
            .Include(x => x.VasItems)
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<LogisticsOperation>> GetAllScalarsAsync(CancellationToken cancellationToken = default)
        => await context.Operations
            .AsNoTracking()
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync(cancellationToken);

    public async Task<LogisticsOperation> AddAsync(LogisticsOperation entity, CancellationToken cancellationToken = default)
    {
        context.Operations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(LogisticsOperation entity, CancellationToken cancellationToken = default)
    {
        context.Operations.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Operations.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Operations.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
