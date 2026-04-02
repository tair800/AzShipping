using MediatR;

namespace Settings.Application.Features.MessageLogs.Queries.GetPaged;

public sealed record GetMessageLogsPagedQuery(
    string? CompanyName,
    string? Receiver,
    DateTime? DateFrom,
    DateTime? DateTo,
    int Page = 1,
    int PageSize = 50) : IRequest<GetMessageLogsPagedResult>;

public sealed record GetMessageLogsPagedResult(IReadOnlyList<MessageLogDto> Items, int Total);
