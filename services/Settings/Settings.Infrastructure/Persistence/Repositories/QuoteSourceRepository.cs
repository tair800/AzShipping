using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class QuoteSourceRepository(SettingsDbContext context) : IQuoteSourceRepository
{
    public async Task<QuoteSource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.QuoteSources.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<QuoteSource>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.QuoteSources.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<QuoteSource> AddAsync(QuoteSource entity, CancellationToken cancellationToken = default)
    {
        context.QuoteSources.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(QuoteSource entity, CancellationToken cancellationToken = default)
    {
        context.QuoteSources.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.QuoteSources.FindAsync([id], cancellationToken);
        if (e != null) { context.QuoteSources.Remove(e); await context.SaveChangesAsync(cancellationToken); }
    }
}
