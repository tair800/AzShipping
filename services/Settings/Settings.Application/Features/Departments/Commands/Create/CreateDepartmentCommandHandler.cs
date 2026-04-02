using MediatR;
using Settings.Application.DTOs.Department;
using Settings.Application.Features.Departments;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Application.Features.Departments.Commands.Create;

public sealed class CreateDepartmentCommandHandler(IDepartmentRepository repository) : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Department
        {
            Id = Guid.NewGuid(),
            CompanyId = request.Dto.CompanyId,
            Name = request.Dto.Name,
            Prefix = request.Dto.Prefix,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return DepartmentMapper.MapToDto(loaded ?? entity);
    }
}
