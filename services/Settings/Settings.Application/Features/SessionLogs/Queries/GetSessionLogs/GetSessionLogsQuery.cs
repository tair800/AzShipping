using MediatR;

namespace Settings.Application.Features.SessionLogs.Queries.GetSessionLogs;

public sealed record GetSessionLogsQuery(
    DateTime? DateFrom,
    DateTime? DateTo,
    Guid? EmployeeId,
    string? EmployeeName) : IRequest<GetSessionLogsResult>;
