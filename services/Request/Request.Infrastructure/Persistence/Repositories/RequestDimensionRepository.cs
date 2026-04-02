using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class RequestDimensionRepository(RequestDbContext context) : IRequestDimensionRepository
{
    public async Task<IReadOnlyList<RequestDimension>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
        => await context.RequestDimensions
            .Where(x => x.RequestId == requestId)
            .ToListAsync(cancellationToken);

    public async Task AddRangeAsync(IEnumerable<RequestDimension> entities, CancellationToken cancellationToken = default)
    {
        context.RequestDimensions.AddRange(entities);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        var list = await context.RequestDimensions.Where(x => x.RequestId == requestId).ToListAsync(cancellationToken);
        context.RequestDimensions.RemoveRange(list);
        await context.SaveChangesAsync(cancellationToken);
    }
}
