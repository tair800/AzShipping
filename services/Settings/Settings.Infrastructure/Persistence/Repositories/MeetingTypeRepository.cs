using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class MeetingTypeRepository(SettingsDbContext context) : IMeetingTypeRepository
{
    public async Task<MeetingType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.MeetingTypes.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<MeetingType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.MeetingTypes.OrderBy(e => e.Name).ToListAsync(cancellationToken);

    public async Task<MeetingType> AddAsync(MeetingType entity, CancellationToken cancellationToken = default)
    {
        context.MeetingTypes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(MeetingType entity, CancellationToken cancellationToken = default)
    {
        context.MeetingTypes.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.MeetingTypes.FindAsync([id], cancellationToken);
        if (e != null) { context.MeetingTypes.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
