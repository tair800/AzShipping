using MediatR;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Application.Features.EmployeeGroups;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups.Commands.Update;

public sealed class UpdateEmployeeGroupCommandHandler(IEmployeeGroupRepository repository)
    : IRequestHandler<UpdateEmployeeGroupCommand, EmployeeGroupDetailDto?>
{
    public async Task<EmployeeGroupDetailDto?> Handle(UpdateEmployeeGroupCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetForUpdateAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new InvalidOperationException("Group name is required.");

        entity.Name = dto.Name.Trim();
        entity.CompanyId = dto.CompanyId;
        entity.PermissionsJson = EmployeeGroupPermissionJson.Normalize(dto.PermissionsJson);
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return EmployeeGroupMapper.ToDetail(loaded ?? entity);
    }
}
