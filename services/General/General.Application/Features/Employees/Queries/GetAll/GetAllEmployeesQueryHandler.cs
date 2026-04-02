using General.Application.DTOs.Employee;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetAll;

public class GetAllEmployeesQueryHandler(IEmployeeRepository repository, ISettingsCatalogLookup catalogLookup)
    : IRequestHandler<GetAllEmployeesQuery, IReadOnlyList<EmployeeDto>>
{
    public async Task<IReadOnlyList<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return await catalogLookup.ToEmployeeDtosAsync(list, cancellationToken);
    }
}
