using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using Identity.Domain.AggregatesModel.UserAggregate.Events;
using Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;
using Identity.Domain.JoinEntities;
using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;

namespace Identity.Domain.AggregatesModel.UserAggregate;

public sealed class User : Entity<long>, IAggregateRoot
{
    public Username Username { get; private set; } = null!;
    public PasswordHash PasswordHash { get; private set; } = null!;
    public FullName? FullName { get; private set; }
    public Email Email { get; private set; } = null!;
    public PhoneNumber? PhoneNumber { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime? LastLoginDate { get; private set; }
    public UserStatus Status { get; private set; } = null!;

    public Guid? CompanyId { get; private set; }
    public Guid? DepartmentId { get; private set; }
    public Guid? WorkerPostId { get; private set; }

    public List<Guid> EmployeeGroupIds { get; private set; } = [];

    public string? EmployeePrefix { get; private set; }
    public bool UnlimitedAccess { get; private set; }
    public bool IsEmployee { get; private set; }
    public DateTime? AccessSince { get; private set; }

    public List<string> AdditionalEmails { get; private set; } = [];
    public List<string> AdditionalPhones { get; private set; } = [];

    public string? Fax { get; private set; }
    public string? Skype { get; private set; }
    public string? SipNumber { get; private set; }
    public string? SignatureRelativePath { get; private set; }

    private readonly List<UserRole> _userRoles = [];
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public string? EmailConfirmationToken { get; private set; }
    public DateTime? EmailConfirmationTokenExpiresAt { get; private set; }

    public string? PasswordResetToken { get; private set; }
    public DateTime? PasswordResetTokenExpiresAt { get; private set; }

    private User() { }
    private User(Username username, PasswordHash passwordHash, FullName? fullName, Email email, PhoneNumber? phoneNumber, IReadOnlyCollection<long> roleIds)
    {
        Username = username ?? throw new DomainException("Username is required");
        PasswordHash = passwordHash ?? throw new DomainException("PasswordHash is required");
        FullName = fullName;
        Email = email ?? throw new DomainException("Email is required");
        PhoneNumber = phoneNumber;
        CreationDate = DateTime.UtcNow;
        Status = UserStatus.Pending;

        SetRoles(roleIds);

        AddDomainEvent(new UserCreatedDomainEvent(Email.Value, Username.Value));
    }

    public static User Create(Username username, PasswordHash passwordHash, FullName? fullName, Email email, PhoneNumber? phoneNumber, IReadOnlyCollection<long> roleIds)
        => new(username, passwordHash, fullName, email, phoneNumber, roleIds);

    public void ApplyExtendedProfile(
        Guid? companyId,
        Guid? departmentId,
        Guid? workerPostId,
        IReadOnlyCollection<Guid> employeeGroupIds,
        string? employeePrefix,
        bool unlimitedAccess,
        bool isEmployee,
        DateTime? accessSince,
        IReadOnlyCollection<string> additionalEmails,
        IReadOnlyCollection<string> additionalPhones,
        string? fax,
        string? skype,
        string? sipNumber)
    {
        CompanyId = companyId;
        DepartmentId = departmentId;
        WorkerPostId = workerPostId;

        EmployeeGroupIds = [.. employeeGroupIds.Distinct()];

        EmployeePrefix = string.IsNullOrWhiteSpace(employeePrefix) ? null : employeePrefix.Trim();
        UnlimitedAccess = unlimitedAccess;
        IsEmployee = isEmployee;
        AccessSince = accessSince;

        AdditionalEmails = [.. additionalEmails.Select(e => e.Trim().ToLowerInvariant()).Where(e => e.Length > 0).Distinct()];
        AdditionalPhones = [.. additionalPhones.Select(p => p.Trim()).Where(p => p.Length > 0)];

        Fax = string.IsNullOrWhiteSpace(fax) ? null : fax.Trim();
        Skype = string.IsNullOrWhiteSpace(skype) ? null : skype.Trim();
        SipNumber = string.IsNullOrWhiteSpace(sipNumber) ? null : sipNumber.Trim();
    }

    public void SetSignatureRelativePath(string? relativePath)
    {
        SignatureRelativePath = string.IsNullOrWhiteSpace(relativePath) ? null : relativePath.Trim();
    }

    public void ApplyStatus(UserStatus newStatus) => Status = newStatus;

    public void MarkDeleted() => ApplyStatus(UserStatus.Deleted);

    public void SetDeactivated() => ApplyStatus(UserStatus.Deactivated);

    public void Activate() => ApplyStatus(UserStatus.Active);

    public void Block() => ApplyStatus(UserStatus.Blocked);

    public void ChangeEmail(Email email)
    {
        Email = email ?? throw new DomainException("Email is required");
    }
    public void ChangePassword(PasswordHash passwordHash)
    {
        PasswordHash = passwordHash ?? throw new DomainException("PasswordHash is required");
    }

    public void UpdateProfile(Username username, FullName? fullName, PhoneNumber? phoneNumber, IReadOnlyCollection<long> roleIds)
    {
        Username = username;
        FullName = fullName;
        PhoneNumber = phoneNumber;

        SetRoles(roleIds);
    }

    private void SetRoles(IReadOnlyCollection<long> roleIds)
    {
        if (roleIds.Count == 0) throw new DomainException("User must have at least one role");

        _userRoles.Clear();

        foreach (var id in roleIds.Distinct())
            _userRoles.Add(new UserRole(id));
    }

    public void MarkLoggedIn(DateTime loggedAtUtc)
    {
        if (loggedAtUtc == default)
            throw new DomainException("Login date is required");

        if (loggedAtUtc.Kind != DateTimeKind.Utc)
            throw new DomainException("Login date must be in UTC");

        if (loggedAtUtc < CreationDate)
            throw new DomainException("Login date cannot be earlier than creation date");

        if (loggedAtUtc > DateTime.UtcNow)
            throw new DomainException("Login date cannot be in the future");

        LastLoginDate = loggedAtUtc;
    }

    public void SetEmailConfirmationToken(string token, DateTime expiresAtUtc)
    {
        if (string.IsNullOrEmpty(token))
            throw new DomainException("Confirmation token is required");

        if (expiresAtUtc.Kind != DateTimeKind.Utc)
            throw new DomainException("Expiration date must be in UTC");

        if (expiresAtUtc <= DateTime.UtcNow)
            throw new DomainException("Expiration date must be in the future");

        EmailConfirmationToken = token;
        EmailConfirmationTokenExpiresAt = expiresAtUtc;
    }

    public void ClearEmailConfirmationToken()
    {
        EmailConfirmationToken = null;
        EmailConfirmationTokenExpiresAt = null;
    }

    public void SetPasswordResetToken(string token, DateTime expiresAtUtc)
    {
        if (string.IsNullOrEmpty(token))
            throw new DomainException("Reset token is required");

        if (expiresAtUtc.Kind != DateTimeKind.Utc)
            throw new DomainException("Expiration date must be in UTC");

        if (expiresAtUtc <= DateTime.UtcNow)
            throw new DomainException("Expiration date must be in the future");

        PasswordResetToken = token;
        PasswordResetTokenExpiresAt = expiresAtUtc;
    }

    public void ClearPasswordResetToken()
    {
        PasswordResetToken = null;
        PasswordResetTokenExpiresAt = null;
    }
}
