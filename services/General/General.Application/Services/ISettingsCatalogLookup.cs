using General.Application.DTOs.Employee;
using General.Domain.AggregatesModel.EmployeeAggregate;

namespace General.Application.Services;

/// <summary>
/// Resolves reference labels from Settings.API (departments, worker posts).
/// </summary>
public interface ISettingsCatalogLookup
{
    Task<EmployeeDto> ToEmployeeDtoAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmployeeDto>> ToEmployeeDtosAsync(IReadOnlyList<Employee> employees, CancellationToken cancellationToken = default);
    Task<EmployeeSummaryDto> ToEmployeeSummaryAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmployeeSummaryDto>> ToEmployeeSummariesAsync(IReadOnlyList<Employee> employees, CancellationToken cancellationToken = default);
}
