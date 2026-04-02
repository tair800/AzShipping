using Microsoft.EntityFrameworkCore;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Infrastructure.Persistence.Repositories;

public class OperationVasRepository(OperationDbContext context) : IOperationVasRepository
{
    public async Task<IReadOnlyList<OperationVas>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
        => await context.OperationVas.AsNoTracking()
            .Where(x => x.OperationId == operationId)
            .ToListAsync(cancellationToken);

    public async Task AddRangeAsync(IReadOnlyList<OperationVas> items, CancellationToken cancellationToken = default)
    {
        context.OperationVas.AddRange(items);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
    {
        var rows = await context.OperationVas.Where(x => x.OperationId == operationId).ToListAsync(cancellationToken);
        if (rows.Count == 0) return;
        context.OperationVas.RemoveRange(rows);
        await context.SaveChangesAsync(cancellationToken);
    }
}
