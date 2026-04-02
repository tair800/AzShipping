namespace Settings.Domain.AggregatesModel.MessageLogAggregate;

public interface IMessageLogRepository
{
    Task AddAsync(MessageLog entity, CancellationToken cancellationToken = default);
    Task<(IReadOnlyList<MessageLog> Items, int Total)> GetPagedAsync(
        string? companyName,
        string? receiver,
        DateTime? dateFrom,
        DateTime? dateTo,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}
