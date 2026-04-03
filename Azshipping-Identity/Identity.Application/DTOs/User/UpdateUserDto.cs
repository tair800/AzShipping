namespace Identity.Application.DTOs.User;

public record UpdateUserDto
(
    long Id,

    string Username,

    string Name,
    string Surname,

    string Phone,

    IReadOnlyCollection<long> RoleIds,

    Guid? CompanyId = null,
    Guid? DepartmentId = null,
    Guid? WorkerPostId = null,

    IReadOnlyCollection<Guid>? EmployeeGroupIds = null,

    string? EmployeePrefix = null,

    bool UnlimitedAccess = false,
    bool IsEmployee = false,

    DateTime? AccessSince = null,

    IReadOnlyCollection<string>? AdditionalEmails = null,
    IReadOnlyCollection<string>? AdditionalPhones = null,

    string? Fax = null,
    string? Skype = null,
    string? SipNumber = null
);
