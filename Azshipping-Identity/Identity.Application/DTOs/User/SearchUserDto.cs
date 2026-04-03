namespace Identity.Application.DTOs.User;

public record SearchUserDto
(
    string? Id,

    string? Username,

    string? Name,
    string? Surname,

    string? Email,

    string? PhoneNumber,

    DateTime? CreationDate,
    DateTime? LastLoginDate,

    string? Status,

    string? CompanyId,
    string? DepartmentId,
    string? WorkerPostId
);
