using MediatR;
using Settings.Application.DTOs.SystemLog;

namespace Settings.Application.Features.SystemLogs.Queries.GetPaged;

public sealed record GetSystemLogsPagedQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    string? Name,
    string? Level,
    int Page = 1,
    int PageSize = 50) : IRequest<GetSystemLogsPagedResult>;

public sealed record GetSystemLogsPagedResult(IReadOnlyList<SystemLogDto> Items, int Total);
