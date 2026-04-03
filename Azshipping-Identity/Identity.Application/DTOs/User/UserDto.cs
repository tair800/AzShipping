using MrStyx.Domain.SeedWork.Utils;

namespace Identity.Application.DTOs.User;

public record UserDto
(
    long Id,

    string Username,

    string Name,
    string Surname,

    string Email,
    string Phone,

    DateTime CreationDate,
    DateTime? LastLoginDate,

    string Status,

    IReadOnlyCollection<long> RoleIds,

    Guid? CompanyId,
    Guid? DepartmentId,
    Guid? WorkerPostId,

    IReadOnlyList<Guid> EmployeeGroupIds,

    string? EmployeePrefix,

    bool UnlimitedAccess,
    bool IsEmployee,

    DateTime? AccessSince,

    IReadOnlyList<string> AdditionalEmails,
    IReadOnlyList<string> AdditionalPhones,

    string? Fax,
    string? Skype,
    string? SipNumber,
    string? SignatureRelativePath,

    string? CompanyName,
    string? DepartmentName,
    string? WorkerPostName,
    /// <summary>Display label for the Groups column (role names from Identity, comma-separated).</summary>
    string? GroupsDisplay
);

public record UserList(IReadOnlyCollection<UserDto> Users);

public record PagedUserList(UserList Items, PaginationMeta Meta);

public record UserLicenseStatsDto(int ActivatedLicenses, int? MaxLicenses, int? FreeLicenses);
