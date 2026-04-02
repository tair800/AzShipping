using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.SystemLogAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class SystemLogRepository(SettingsDbContext context) : ISystemLogRepository
{
    public async Task<(IReadOnlyList<SystemLog> Items, int Total)> GetPagedAsync(
        DateTime? dateFrom, DateTime? dateTo, string? name, string? level,
        int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var q = context.SystemLogs.AsNoTracking();
        if (dateFrom.HasValue)
            q = q.Where(x => x.CreatedAt >= dateFrom.Value);
        if (dateTo.HasValue)
        {
            var end = dateTo.Value.Date.AddDays(1);
            q = q.Where(x => x.CreatedAt < end);
        }
        if (!string.IsNullOrWhiteSpace(name))
            q = q.Where(x => x.Name.Contains(name));
        if (!string.IsNullOrWhiteSpace(level))
            q = q.Where(x => x.Level == level);

        var total = await q.CountAsync(cancellationToken);
        var items = await q
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task AddAsync(SystemLog entity, CancellationToken cancellationToken = default)
    {
        context.SystemLogs.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
