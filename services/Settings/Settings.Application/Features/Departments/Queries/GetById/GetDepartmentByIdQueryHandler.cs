using MediatR;
using Settings.Application.DTOs.Department;
using Settings.Application.Features.Departments;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Application.Features.Departments.Queries.GetById;

public sealed class GetDepartmentByIdQueryHandler(IDepartmentRepository repository) : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto?>
{
    public async Task<DepartmentDto?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return DepartmentMapper.MapToDto(entity);
    }
}
