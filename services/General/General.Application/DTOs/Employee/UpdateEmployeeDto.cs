namespace General.Application.DTOs.Employee;

public record UpdateEmployeeDto
{
    public long UserId { get; init; }
    public string? FullName { get; init; }
    public string? Username { get; init; }
    public Guid? DepartmentId { get; init; }
    public Guid? WorkerPostId { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? ContractNumber { get; init; }
    public string? Address { get; init; }
    public string? ProfileImageUrl { get; init; }
}
