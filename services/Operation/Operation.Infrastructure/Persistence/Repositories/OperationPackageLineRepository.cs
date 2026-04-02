using Microsoft.EntityFrameworkCore;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Infrastructure.Persistence.Repositories;

public class OperationPackageLineRepository(OperationDbContext context) : IOperationPackageLineRepository
{
    public async Task<IReadOnlyList<OperationPackageLine>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
        => await context.OperationPackageLines.AsNoTracking()
            .Where(x => x.OperationId == operationId)
            .OrderBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);

    public async Task AddRangeAsync(IReadOnlyList<OperationPackageLine> items, CancellationToken cancellationToken = default)
    {
        context.OperationPackageLines.AddRange(items);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
    {
        var rows = await context.OperationPackageLines.Where(x => x.OperationId == operationId).ToListAsync(cancellationToken);
        if (rows.Count == 0) return;
        context.OperationPackageLines.RemoveRange(rows);
        await context.SaveChangesAsync(cancellationToken);
    }
}
