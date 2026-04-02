using MediatR;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Application.Features.EmployeeGroups;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups.Commands.Clone;

public sealed class CloneEmployeeGroupCommandHandler(IEmployeeGroupRepository repository)
    : IRequestHandler<CloneEmployeeGroupCommand, EmployeeGroupDetailDto?>
{
    public async Task<EmployeeGroupDetailDto?> Handle(CloneEmployeeGroupCommand request, CancellationToken cancellationToken)
    {
        var source = await repository.GetByIdAsync(request.SourceId, cancellationToken);
        if (source == null) return null;

        var copyName = source.Name.TrimEnd() + " (copy)";
        var entity = new EmployeeGroup
        {
            Id = Guid.NewGuid(),
            Name = copyName,
            CompanyId = source.CompanyId,
            PermissionsJson = EmployeeGroupPermissionJson.Normalize(source.PermissionsJson),
            CreatedAtUtc = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return EmployeeGroupMapper.ToDetail(loaded ?? entity);
    }
}
