using MediatR;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Application.Features.EmployeeGroups;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups.Commands.Create;

public sealed class CreateEmployeeGroupCommandHandler(IEmployeeGroupRepository repository)
    : IRequestHandler<CreateEmployeeGroupCommand, EmployeeGroupDetailDto>
{
    public async Task<EmployeeGroupDetailDto> Handle(CreateEmployeeGroupCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new InvalidOperationException("Group name is required.");

        var now = DateTime.UtcNow;
        var entity = new EmployeeGroup
        {
            Id = Guid.NewGuid(),
            Name = dto.Name.Trim(),
            CompanyId = dto.CompanyId,
            PermissionsJson = EmployeeGroupPermissionJson.Normalize(dto.PermissionsJson),
            CreatedAtUtc = now
        };

        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return EmployeeGroupMapper.ToDetail(loaded ?? entity);
    }
}
