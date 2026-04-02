using MediatR;
using Settings.Application.DTOs.Department;
using Settings.Application.Features.Departments;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Application.Features.Departments.Commands.Update;

public sealed class UpdateDepartmentCommandHandler(IDepartmentRepository repository) : IRequestHandler<UpdateDepartmentCommand, DepartmentDto?>
{
    public async Task<DepartmentDto?> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.CompanyId = request.Dto.CompanyId;
        entity.Name = request.Dto.Name;
        entity.Prefix = request.Dto.Prefix;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return DepartmentMapper.MapToDto(loaded ?? entity);
    }
}
