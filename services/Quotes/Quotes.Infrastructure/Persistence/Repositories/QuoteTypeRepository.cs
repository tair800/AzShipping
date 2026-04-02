using Microsoft.EntityFrameworkCore;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Infrastructure.Persistence.Repositories;

public class QuoteTypeRepository(QuotesDbContext context) : IQuoteTypeRepository
{
    public async Task<QuoteType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.QuoteTypes.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<QuoteType>> GetAllActiveAsync(CancellationToken cancellationToken = default)
        => await context.QuoteTypes
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<QuoteType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.QuoteTypes
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);
}
