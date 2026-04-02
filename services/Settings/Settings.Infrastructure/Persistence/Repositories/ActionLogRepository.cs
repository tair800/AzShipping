using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.ActionLogAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class ActionLogRepository(SettingsDbContext context) : IActionLogRepository
{
    public async Task<(IReadOnlyList<ActionLog> Items, int Total)> GetPagedAsync(
        DateTime? dateFrom, DateTime? dateTo, Guid? employeeId, string? employeeName, string? action,
        string? orderFilter, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var q = context.ActionLogs.AsNoTracking();
        if (dateFrom.HasValue)
            q = q.Where(x => x.CreatedAt >= dateFrom.Value);
        if (dateTo.HasValue)
        {
            var end = dateTo.Value.Date.AddDays(1);
            q = q.Where(x => x.CreatedAt < end);
        }
        if (employeeId.HasValue)
            q = q.Where(x => x.EmployeeId == employeeId.Value);
        if (!string.IsNullOrWhiteSpace(employeeName))
            q = q.Where(x => x.EmployeeName != null && x.EmployeeName.Contains(employeeName));
        if (!string.IsNullOrWhiteSpace(action))
            q = q.Where(x => x.Action == action);
        if (!string.IsNullOrWhiteSpace(orderFilter))
            q = q.Where(x => x.Data.Contains(orderFilter));

        var total = await q.CountAsync(cancellationToken);
        var items = await q
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task<IReadOnlyList<string>> GetDistinctActionsAsync(CancellationToken cancellationToken = default)
    {
        return await context.ActionLogs
            .AsNoTracking()
            .Select(x => x.Action)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ActionLog entity, CancellationToken cancellationToken = default)
    {
        context.ActionLogs.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ActionLog>> GetForSessionLogAsync(DateTime? dateFrom, DateTime? dateTo, Guid? employeeId, string? employeeName, int maxEntries = 50000, CancellationToken cancellationToken = default)
    {
        var q = context.ActionLogs.AsNoTracking()
            .Where(x => x.SessionId != null && x.SessionId != "");
        if (dateFrom.HasValue)
            q = q.Where(x => x.CreatedAt >= dateFrom.Value);
        if (dateTo.HasValue)
        {
            var end = dateTo.Value.Date.AddDays(1);
            q = q.Where(x => x.CreatedAt < end);
        }
        if (employeeId.HasValue)
            q = q.Where(x => x.EmployeeId == employeeId.Value);
        if (!string.IsNullOrWhiteSpace(employeeName))
            q = q.Where(x => x.EmployeeName != null && x.EmployeeName.Contains(employeeName));
        return await q
            .OrderBy(x => x.CreatedAt)
            .Take(maxEntries)
            .ToListAsync(cancellationToken);
    }
}
