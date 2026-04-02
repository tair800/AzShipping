using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.MessageLogAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class MessageLogRepository(SettingsDbContext context) : IMessageLogRepository
{
    public async Task AddAsync(MessageLog entity, CancellationToken cancellationToken = default)
    {
        context.MessageLogs.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<MessageLog> Items, int Total)> GetPagedAsync(
        string? companyName,
        string? receiver,
        DateTime? dateFrom,
        DateTime? dateTo,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var q = context.MessageLogs.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(companyName))
            q = q.Where(x => x.CompanyName != null && x.CompanyName.Contains(companyName));
        if (!string.IsNullOrWhiteSpace(receiver))
            q = q.Where(x => x.Receiver.Contains(receiver));
        if (dateFrom.HasValue)
            q = q.Where(x => x.SentAt >= dateFrom.Value);
        if (dateTo.HasValue)
        {
            var end = dateTo.Value.Date.AddDays(1);
            q = q.Where(x => x.SentAt < end);
        }

        var total = await q.CountAsync(cancellationToken);
        var items = await q
            .OrderByDescending(x => x.SentAt)
            .ThenByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return (items, total);
    }
}
