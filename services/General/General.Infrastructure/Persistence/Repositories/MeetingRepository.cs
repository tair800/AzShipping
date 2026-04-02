using General.Domain.AggregatesModel.MeetingAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class MeetingRepository(GeneralDbContext context) : IMeetingRepository
{
    public async Task<Meeting?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Meetings.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Meeting>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Meetings.OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<Meeting> AddAsync(Meeting entity, CancellationToken cancellationToken = default)
    {
        await context.Meetings.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Meeting entity, CancellationToken cancellationToken = default)
    {
        context.Meetings.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Meetings.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Meetings.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
