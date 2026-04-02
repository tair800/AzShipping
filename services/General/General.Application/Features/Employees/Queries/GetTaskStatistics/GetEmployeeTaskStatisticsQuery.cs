using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetTaskStatistics;

/// <param name="WeekStartUtc">Optional Monday 00:00 UTC; if null, current week is used.</param>
/// <param name="CompletedStatusIds">Task status GUIDs from Settings that count as done. If empty, completed counts are zero.</param>
public record GetEmployeeTaskStatisticsQuery(Guid EmployeeId, DateTime? WeekStartUtc, IReadOnlyList<Guid> CompletedStatusIds)
    : IRequest<EmployeeTaskStatisticsDto?>;
