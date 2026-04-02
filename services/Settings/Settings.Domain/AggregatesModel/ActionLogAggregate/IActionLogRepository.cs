namespace Settings.Domain.AggregatesModel.ActionLogAggregate;

public interface IActionLogRepository
{
    Task<(IReadOnlyList<ActionLog> Items, int Total)> GetPagedAsync(DateTime? dateFrom, DateTime? dateTo, Guid? employeeId, string? employeeName, string? action, string? orderFilter, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetDistinctActionsAsync(CancellationToken cancellationToken = default);
    Task AddAsync(ActionLog entity, CancellationToken cancellationToken = default);
    /// <summary>Returns ActionLog entries with SessionId, for session log aggregation. Ordered by CreatedAt.</summary>
    Task<IReadOnlyList<ActionLog>> GetForSessionLogAsync(DateTime? dateFrom, DateTime? dateTo, Guid? employeeId, string? employeeName, int maxEntries = 50000, CancellationToken cancellationToken = default);
}
