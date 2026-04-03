namespace Identity.Application.DTOs.User;

public record CreateUserDto
(
    string Username,
    /// <summary>When null or empty, a strong random password is generated server-side.</summary>
    string? Password,

    string Name,
    string Surname,

    string Email,
    string Phone,

    IReadOnlyCollection<long> RoleIds,

    Guid? CompanyId = null,
    Guid? DepartmentId = null,
    Guid? WorkerPostId = null,

    IReadOnlyCollection<Guid>? EmployeeGroupIds = null,

    string? EmployeePrefix = null,

    bool UnlimitedAccess = false,
    bool IsEmployee = false,

    /// <summary>Defaults to UTC now when omitted (legacy create payloads without this field).</summary>
    DateTime? AccessSince = null,

    IReadOnlyCollection<string>? AdditionalEmails = null,
    IReadOnlyCollection<string>? AdditionalPhones = null,

    string? Fax = null,
    string? Skype = null,
    string? SipNumber = null,

    /// <summary>When true, user is activated immediately (subject to license cap).</summary>
    bool ActivateImmediately = false
);
