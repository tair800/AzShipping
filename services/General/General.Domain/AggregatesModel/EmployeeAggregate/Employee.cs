namespace General.Domain.AggregatesModel.EmployeeAggregate;

/// <summary>
/// HR profile for a user. <see cref="UserId"/> matches Identity <c>User.Id</c> (<c>long</c>) and JWT <c>uid</c> (same number as string).
/// </summary>
public class Employee
{
    public Guid Id { get; set; }
    public long UserId { get; set; }

    public string? FullName { get; set; }
    public string? Username { get; set; }

    /// <summary>Settings <c>Departments</c> row id (<c>/api/departments</c>).</summary>
    public Guid? DepartmentId { get; set; }

    /// <summary>Settings <c>WorkerPosts</c> row id (<c>/api/workerposts</c>) — worker position.</summary>
    public Guid? WorkerPostId { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? ContractNumber { get; set; }
    public string? Address { get; set; }
    public string? ProfileImageUrl { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
}
