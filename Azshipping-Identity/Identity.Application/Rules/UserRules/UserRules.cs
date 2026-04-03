using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using MrStyx.Application.Exceptions;

namespace Identity.Application.Rules.UserRules;

public class UserRules(IUserRepository userRepository, IRoleRepository roleRepository) : IUserRules
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task UsernameUniquenessCheck(string username, CancellationToken cancellationToken)
    {
        var existingUsername = await _userRepository.GetFirstOrDefaultAsync(u => u.Username.Value == username, cancellationToken);
        if (existingUsername is not null) throw new ConflictException("Username already exists");
    }

    public async Task UsernameUniquenessCheck(string username, long userId, CancellationToken cancellationToken)
    {
        var existingUsername = await _userRepository.GetFirstOrDefaultAsync(u => u.Username.Value == username && u.Id != userId, cancellationToken);
        if (existingUsername is not null) throw new ConflictException("Username already exists");
    }
    public async Task EmailUniquenessCheck(string email, CancellationToken cancellationToken)
    {
        var existingEmail = await _userRepository.GetFirstOrDefaultAsync(u => u.Email.Value == email, cancellationToken);
        if (existingEmail is not null) throw new ConflictException("Email already exists");
    }

    public async Task EmailUniquenessCheck(string email, long userId, CancellationToken cancellationToken)
    {
        var existingEmail = await _userRepository.GetFirstOrDefaultAsync(u => u.Email.Value == email && u.Id != userId, cancellationToken);
        if (existingEmail is not null) throw new ConflictException("Email already exists");
    }

    public async Task EmailCollectionUniquenessCheck(string primaryEmail, IReadOnlyCollection<string> additionalEmails, long? excludingUserId, CancellationToken cancellationToken)
    {
        var emails = new List<string> { primaryEmail };
        emails.AddRange(additionalEmails);
        foreach (var email in emails
                     .Where(e => !string.IsNullOrWhiteSpace(e))
                     .Select(e => e.Trim())
                     .Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var normalized = email.Trim().ToLowerInvariant();
            // Do not use string.Equals(..., StringComparison) — EF Core cannot translate it to SQL.
            var existing = await _userRepository.GetFirstOrDefaultAsync(u =>
                    (!excludingUserId.HasValue || u.Id != excludingUserId) &&
                    (u.Email.Value.ToLower() == normalized
                     || u.AdditionalEmails.Any(e => e.ToLower() == normalized)),
                cancellationToken);
            if (existing is not null)
                throw new ConflictException($"Email '{email}' is already in use");
        }
    }

    public async Task FindMissingRoles(IReadOnlyCollection<long> roleIds, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetByIdsAsync(roleIds, cancellationToken);
        var found = roles.Select(r => r.Id).ToHashSet();

        var missing = roleIds.Where(id => !found.Contains(id)).ToList();

        if (missing.Count != 0)
            throw new NotFoundException($"Can not find roles by this ids: {string.Join(", ", missing)}");
    }
}