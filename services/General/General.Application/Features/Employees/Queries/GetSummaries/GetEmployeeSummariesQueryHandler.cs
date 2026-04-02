using General.Application.DTOs.Employee;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetSummaries;

public class GetEmployeeSummariesQueryHandler(IEmployeeRepository repository, ISettingsCatalogLookup catalogLookup)
    : IRequestHandler<GetEmployeeSummariesQuery, IReadOnlyList<EmployeeSummaryDto>>
{
    public async Task<IReadOnlyList<EmployeeSummaryDto>> Handle(GetEmployeeSummariesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return await catalogLookup.ToEmployeeSummariesAsync(list, cancellationToken);
    }
}
