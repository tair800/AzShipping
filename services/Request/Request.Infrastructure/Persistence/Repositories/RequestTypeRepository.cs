using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class RequestTypeRepository(RequestDbContext context) : IRequestTypeRepository
{
    public async Task<IReadOnlyList<RequestType>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        => await context.RequestTypes
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<RequestType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.RequestTypes
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);

    public async Task<RequestType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        => await context.RequestTypes
            .FirstOrDefaultAsync(x => x.Code == code && x.IsActive, cancellationToken);

    public async Task<RequestType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.RequestTypes.FindAsync([id], cancellationToken);

    public async Task<RequestType> AddAsync(RequestType entity, CancellationToken cancellationToken = default)
    {
        context.RequestTypes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(RequestType entity, CancellationToken cancellationToken = default)
    {
        context.RequestTypes.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.RequestTypes.FindAsync([id], cancellationToken);
        if (e != null)
        {
            e.IsActive = false;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
