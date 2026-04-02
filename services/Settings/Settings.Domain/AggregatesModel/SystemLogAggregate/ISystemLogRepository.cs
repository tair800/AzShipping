namespace Settings.Domain.AggregatesModel.SystemLogAggregate;

public interface ISystemLogRepository
{
    Task<(IReadOnlyList<SystemLog> Items, int Total)> GetPagedAsync(DateTime? dateFrom, DateTime? dateTo, string? name, string? level, int page, int pageSize, CancellationToken cancellationToken = default);
    Task AddAsync(SystemLog entity, CancellationToken cancellationToken = default);
}
