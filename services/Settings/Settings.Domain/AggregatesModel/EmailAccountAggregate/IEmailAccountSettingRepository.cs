namespace Settings.Domain.AggregatesModel.EmailAccountAggregate;

public interface IEmailAccountSettingRepository
{
    Task<EmailAccountSetting?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    /// <summary>Tracked entity for updates (includes protected password bytes).</summary>
    Task<EmailAccountSetting?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EmailAccountSetting?> GetByAccountEmailAsync(string accountEmail, CancellationToken cancellationToken = default);
    /// <summary>First row with <see cref="EmailAccountSetting.IsSystemEmail"/> (oldest by creation), for Identity / system outbound relay.</summary>
    Task<EmailAccountSetting?> GetFirstSystemMailboxAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmailAccountSetting>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmailAccountSetting> AddAsync(EmailAccountSetting entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmailAccountSetting entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
