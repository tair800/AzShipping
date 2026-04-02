using MediatR;
using Settings.Application.DTOs.ActionLog;

namespace Settings.Application.Features.ActionLogs.Queries.GetPaged;

public sealed record GetActionLogsPagedQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    Guid? EmployeeId,
    string? EmployeeName,
    string? Action,
    string? OrderFilter,
    int Page = 1,
    int PageSize = 50) : IRequest<GetActionLogsPagedResult>;

public sealed record GetActionLogsPagedResult(IReadOnlyList<ActionLogDto> Items, int Total);
