using General.Application.DTOs.Employee;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetById;

public class GetEmployeeByIdQueryHandler(IEmployeeRepository repository, ISettingsCatalogLookup catalogLookup)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : await catalogLookup.ToEmployeeDtoAsync(e, cancellationToken);
    }
}
