using General.Application.DTOs.Employee;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using MediatR;

namespace General.Application.Features.Employees.Queries.GetByUserId;

public class GetEmployeeByUserIdQueryHandler(IEmployeeRepository repository, ISettingsCatalogLookup catalogLookup)
    : IRequestHandler<GetEmployeeByUserIdQuery, EmployeeDto?>
{
    public async Task<EmployeeDto?> Handle(GetEmployeeByUserIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByUserIdAsync(request.UserId, cancellationToken);
        return e == null ? null : await catalogLookup.ToEmployeeDtoAsync(e, cancellationToken);
    }
}
