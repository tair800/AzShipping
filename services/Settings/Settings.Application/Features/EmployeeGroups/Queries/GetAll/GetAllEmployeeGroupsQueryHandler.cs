using MediatR;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Application.Features.EmployeeGroups;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups.Queries.GetAll;

public sealed class GetAllEmployeeGroupsQueryHandler(IEmployeeGroupRepository repository)
    : IRequestHandler<GetAllEmployeeGroupsQuery, IReadOnlyList<EmployeeGroupListItemDto>>
{
    public async Task<IReadOnlyList<EmployeeGroupListItemDto>> Handle(GetAllEmployeeGroupsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(request.CompanyId, request.Search, cancellationToken);
        return list.Select(EmployeeGroupMapper.ToListItem).ToList();
    }
}
