using MediatR;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Application.Features.EmployeeGroups;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups.Queries.GetById;

public sealed class GetEmployeeGroupByIdQueryHandler(IEmployeeGroupRepository repository)
    : IRequestHandler<GetEmployeeGroupByIdQuery, EmployeeGroupDetailDto?>
{
    public async Task<EmployeeGroupDetailDto?> Handle(GetEmployeeGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : EmployeeGroupMapper.ToDetail(e);
    }
}
