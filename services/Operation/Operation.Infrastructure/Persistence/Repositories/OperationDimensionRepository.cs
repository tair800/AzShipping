using Microsoft.EntityFrameworkCore;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Infrastructure.Persistence.Repositories;

public class OperationDimensionRepository(OperationDbContext context) : IOperationDimensionRepository
{
    public async Task<IReadOnlyList<OperationDimension>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
        => await context.OperationDimensions.AsNoTracking()
            .Where(x => x.OperationId == operationId)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

    public async Task AddRangeAsync(IReadOnlyList<OperationDimension> items, CancellationToken cancellationToken = default)
    {
        context.OperationDimensions.AddRange(items);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
    {
        var rows = await context.OperationDimensions.Where(x => x.OperationId == operationId).ToListAsync(cancellationToken);
        if (rows.Count > 0)
        {
            context.OperationDimensions.RemoveRange(rows);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
