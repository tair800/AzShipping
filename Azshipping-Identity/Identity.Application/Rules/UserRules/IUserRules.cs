namespace Identity.Application.Rules.UserRules;

public interface IUserRules
{
    Task UsernameUniquenessCheck(string username, CancellationToken cancellationToken);
    Task UsernameUniquenessCheck(string username, long userId, CancellationToken cancellationToken);
    Task EmailUniquenessCheck(string email, CancellationToken cancellationToken);
    Task EmailUniquenessCheck(string email, long userId, CancellationToken cancellationToken);
    Task EmailCollectionUniquenessCheck(string primaryEmail, IReadOnlyCollection<string> additionalEmails, long? excludingUserId, CancellationToken cancellationToken);
    Task FindMissingRoles(IReadOnlyCollection<long> roleIds, CancellationToken cancellationToken);
}