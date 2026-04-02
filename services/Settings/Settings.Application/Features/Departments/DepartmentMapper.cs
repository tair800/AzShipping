using Settings.Application.DTOs.Department;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Application.Features.Departments;

public static class DepartmentMapper
{
    public static DepartmentDto MapToDto(Department? entity)
    {
        if (entity == null) return null!;
        return new DepartmentDto(
            entity.Id,
            entity.CompanyId,
            entity.Company?.Name ?? "",
            entity.Name,
            entity.Prefix,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
