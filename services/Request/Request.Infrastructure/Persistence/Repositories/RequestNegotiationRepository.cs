using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;

namespace Request.Infrastructure.Persistence.Repositories;

public class RequestNegotiationRepository(RequestDbContext context) : IRequestNegotiationRepository
{
    public async Task<RequestNegotiation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.RequestNegotiations.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<RequestNegotiation>> GetAllAsync(Guid? clientIdFilter = null, CancellationToken cancellationToken = default)
    {
        var q = context.RequestNegotiations.AsQueryable();
        if (clientIdFilter.HasValue)
            q = q.Where(x => x.ClientId == clientIdFilter.Value);
        return await q.OrderByDescending(x => x.CreationDate).ToListAsync(cancellationToken);
    }

    public async Task<RequestNegotiation> AddAsync(RequestNegotiation entity, CancellationToken cancellationToken = default)
    {
        context.RequestNegotiations.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(RequestNegotiation entity, CancellationToken cancellationToken = default)
    {
        context.RequestNegotiations.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.RequestNegotiations.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.RequestNegotiations.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
