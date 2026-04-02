using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public sealed class EmailAccountSettingRepository(SettingsDbContext context) : IEmailAccountSettingRepository
{
    public Task<EmailAccountSetting?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => context.EmailAccountSettings.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task<EmailAccountSetting?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
        => context.EmailAccountSettings.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public Task<EmailAccountSetting?> GetByAccountEmailAsync(string accountEmail, CancellationToken cancellationToken = default)
    {
        var n = EmailAccountSetting.NormalizeAccountEmail(accountEmail);
        return context.EmailAccountSettings.AsNoTracking()
            .FirstOrDefaultAsync(e => e.AccountEmail == n, cancellationToken);
    }

    public Task<EmailAccountSetting?> GetFirstSystemMailboxAsync(CancellationToken cancellationToken = default)
        => context.EmailAccountSettings.AsNoTracking()
            .Where(e => e.IsSystemEmail)
            .OrderBy(e => e.CreatedAtUtc)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<EmailAccountSetting>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.EmailAccountSettings.AsNoTracking()
            .OrderBy(e => e.AccountEmail)
            .ToListAsync(cancellationToken);

    public async Task<EmailAccountSetting> AddAsync(EmailAccountSetting entity, CancellationToken cancellationToken = default)
    {
        context.EmailAccountSettings.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(EmailAccountSetting entity, CancellationToken cancellationToken = default)
    {
        context.EmailAccountSettings.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.EmailAccountSettings.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (e != null)
        {
            context.EmailAccountSettings.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

}
