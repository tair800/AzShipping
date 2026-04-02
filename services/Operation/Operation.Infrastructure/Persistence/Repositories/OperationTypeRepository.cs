using Microsoft.EntityFrameworkCore;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Infrastructure.Persistence.Repositories;

public class OperationTypeRepository(OperationDbContext context) : IOperationTypeRepository
{
    public async Task<OperationType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.OperationTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<OperationType>> GetAllAsync(bool includeInactive, CancellationToken cancellationToken = default)
    {
        var q = context.OperationTypes.AsNoTracking().AsQueryable();
        if (!includeInactive)
            q = q.Where(x => x.IsActive);
        return await q.OrderBy(x => x.SortOrder).ThenBy(x => x.Name).ToListAsync(cancellationToken);
    }
}
