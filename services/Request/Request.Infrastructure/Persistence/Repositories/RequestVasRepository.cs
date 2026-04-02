using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class RequestVasRepository(RequestDbContext context) : IRequestVasRepository
{
    public async Task<IReadOnlyList<RequestVas>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
        => await context.RequestVas
            .Where(x => x.RequestId == requestId)
            .ToListAsync(cancellationToken);

    public async Task AddRangeAsync(IEnumerable<RequestVas> entities, CancellationToken cancellationToken = default)
    {
        context.RequestVas.AddRange(entities);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        var list = await context.RequestVas.Where(x => x.RequestId == requestId).ToListAsync(cancellationToken);
        context.RequestVas.RemoveRange(list);
        await context.SaveChangesAsync(cancellationToken);
    }
}
