using General.Application.DTOs.Employee;
using General.Domain.AggregatesModel.EmployeeAggregate;

namespace General.Application.Features.Employees;

public static class EmployeeMapper
{
    /// <summary>Maps entity to DTO without Settings-resolved names; enrich via <c>ISettingsCatalogLookup</c>.</summary>
    public static EmployeeDto ToCoreDto(Employee e) => new()
    {
        Id = e.Id,
        UserId = e.UserId,
        FullName = e.FullName,
        Username = e.Username,
        DepartmentId = e.DepartmentId,
        DepartmentName = null,
        WorkerPostId = e.WorkerPostId,
        WorkerPostName = null,
        Email = e.Email,
        Phone = e.Phone,
        ContractNumber = e.ContractNumber,
        Address = e.Address,
        ProfileImageUrl = e.ProfileImageUrl,
        CreatedAtUtc = e.CreatedAtUtc,
        UpdatedAtUtc = e.UpdatedAtUtc
    };

    public static EmployeeSummaryDto ToCoreSummary(Employee e) => new()
    {
        Id = e.Id,
        UserId = e.UserId,
        FullName = e.FullName,
        Username = e.Username,
        DepartmentId = e.DepartmentId,
        DepartmentName = null,
        WorkerPostId = e.WorkerPostId,
        WorkerPostName = null
    };
}
