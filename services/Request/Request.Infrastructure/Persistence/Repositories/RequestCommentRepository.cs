using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.RequestCommentAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class RequestCommentRepository(RequestDbContext context) : IRequestCommentRepository
{
    public async Task<RequestComment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.RequestComments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<RequestComment>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
        => await context.RequestComments
            .Where(x => x.RequestId == requestId)
            .OrderByDescending(x => x.Date)
            .ToListAsync(cancellationToken);

    public async Task<RequestComment> AddAsync(RequestComment entity, CancellationToken cancellationToken = default)
    {
        context.RequestComments.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(RequestComment entity, CancellationToken cancellationToken = default)
    {
        context.RequestComments.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.RequestComments.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.RequestComments.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
