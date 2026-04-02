using MediatR;
using Settings.Application.DTOs.Department;
using Settings.Application.Features.Departments;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Application.Features.Departments.Queries.GetAll;

public sealed class GetAllDepartmentsQueryHandler(IDepartmentRepository repository) : IRequestHandler<GetAllDepartmentsQuery, IReadOnlyList<DepartmentDto>>
{
    public async Task<IReadOnlyList<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(DepartmentMapper.MapToDto).ToList();
    }
}
