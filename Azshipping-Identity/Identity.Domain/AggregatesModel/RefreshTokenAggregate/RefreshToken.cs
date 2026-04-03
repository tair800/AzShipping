using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;

namespace Identity.Domain.AggregatesModel.RefreshTokenAggregate;

public sealed class RefreshToken : Entity<long>, IAggregateRoot
{
    public long UserId { get; private set; }

    public string TokenHash { get; private set; } = null!;
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime? RevokedAtUtc { get; private set; }
    public string? ReplacedByTokenHash { get; private set; }
    public string? CreatedByIp { get; private set; }
    public string? RevokedByIp { get; private set; }

    public string? Device { get; private set; }

    private RefreshToken() { }

    private RefreshToken(long userId, string tokenHash, DateTime createdAtUtc, DateTime expiresAtUtc, string? createdByIp, string? device)
    {
        if (userId <= 0) throw new DomainException("UserId must be greater than 0");

        UserId = userId;
        TokenHash = tokenHash ?? throw new DomainException("TokenHash is required");
        CreatedAtUtc = ValidateCreationDate(createdAtUtc);
        ExpiresAtUtc = ValidateExpiresAtDate(expiresAtUtc, createdAtUtc);
        CreatedByIp = createdByIp;
        Device = device;

    }

    public static RefreshToken Create(long userId, string tokenHash, DateTime createdAtUtc, DateTime expiresAtUtc, string? createdByIp, string? device)
        => new(userId, tokenHash, createdAtUtc, expiresAtUtc, createdByIp, device);

    public void UpdateRevokeData(DateTime revokedAtUtc, string? revokedByIp, string? replacedByTokenHash = null)
    {
        RevokedAtUtc = ValidateRevokeDate(revokedAtUtc);
        RevokedByIp = revokedByIp;
        ReplacedByTokenHash = replacedByTokenHash;
    }

    private DateTime ValidateRevokeDate(DateTime revokedAtUtc)
    {
        if (revokedAtUtc == default)
            throw new DomainException("Revoke date is required");

        if (revokedAtUtc.Kind != DateTimeKind.Utc)
            throw new DomainException("Revoke date must be in UTC");

        if (revokedAtUtc <= CreatedAtUtc)
            throw new DomainException("Revoke date cant be less than Create date");

        return revokedAtUtc;
    }
    private static DateTime ValidateCreationDate(DateTime createdAtUtc)
    {
        if (createdAtUtc == default)
            throw new DomainException("Creation date is required");

        if (createdAtUtc.Kind != DateTimeKind.Utc)
            throw new DomainException("Creation date must be in UTC");

        if (createdAtUtc > DateTime.UtcNow)
            throw new DomainException("Creation date cannot be in the future");

        return createdAtUtc;
    }

    private static DateTime ValidateExpiresAtDate(DateTime expiresAtUtc, DateTime createdAtUtc)
    {
        if (expiresAtUtc == default)
            throw new DomainException("Expire date is required");

        if (expiresAtUtc.Kind != DateTimeKind.Utc)
            throw new DomainException("Expire date must be in UTC");

        if (expiresAtUtc <= createdAtUtc)
            throw new DomainException("Expire date can not be less than Creation date");

        return expiresAtUtc;
    }
}