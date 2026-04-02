namespace General.Application.DTOs.Employee;

public record EmployeeDto
{
    public Guid Id { get; init; }
    public long UserId { get; init; }
    public string? FullName { get; init; }
    public string? Username { get; init; }
    public Guid? DepartmentId { get; init; }
    public string? DepartmentName { get; init; }
    public Guid? WorkerPostId { get; init; }
    public string? WorkerPostName { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? ContractNumber { get; init; }
    public string? Address { get; init; }
    public string? ProfileImageUrl { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
}

/// <summary>For task responsible-person picker: use <see cref="UserId"/> (Identity user id) as <c>responsibleUserId</c> on tasks.</summary>
public record EmployeeSummaryDto
{
    public Guid Id { get; init; }
    public long UserId { get; init; }
    public string? FullName { get; init; }
    public string? Username { get; init; }
    public Guid? DepartmentId { get; init; }
    public string? DepartmentName { get; init; }
    public Guid? WorkerPostId { get; init; }
    public string? WorkerPostName { get; init; }
}
