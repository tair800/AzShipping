using MediatR;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups.Queries.Resolve;

public sealed class ResolveEmployeeGroupPermissionsQueryHandler(IEmployeeGroupRepository repository)
    : IRequestHandler<ResolveEmployeeGroupPermissionsQuery, ResolveEmployeeGroupPermissionsResponseDto>
{
    private readonly IEmployeeGroupRepository _repository = repository;

    public async Task<ResolveEmployeeGroupPermissionsResponseDto> Handle(
        ResolveEmployeeGroupPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var ids = (request.Ids ?? []).Where(i => i != Guid.Empty).Distinct().ToList();
        if (ids.Count == 0)
            return new ResolveEmployeeGroupPermissionsResponseDto([]);

        var groups = await _repository.GetByIdsAsync(ids, cancellationToken);
        var blobs = groups.Select(g => g.PermissionsJson).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
        var claims = EmployeeGroupPermissionMerger.MergeAndFlatten(blobs);
        return new ResolveEmployeeGroupPermissionsResponseDto(claims);
    }
}
