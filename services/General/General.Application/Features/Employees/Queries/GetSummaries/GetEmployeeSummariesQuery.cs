using General.Application.DTOs.Employee;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetSummaries;

public record GetEmployeeSummariesQuery : IRequest<IReadOnlyList<EmployeeSummaryDto>>;
