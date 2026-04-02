using Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence.Repositories;

public class InvoiceLookupRepository(AccountingDbContext context) : IInvoiceLookupRepository
{
    public async Task<IReadOnlyList<InvoiceLookupOption>> GetActiveAsync(InvoiceLookupCategory? category,
        CancellationToken cancellationToken = default)
    {
        var q = context.InvoiceLookupOptions.AsNoTracking().Where(x => x.IsActive);
        if (category.HasValue)
            q = q.Where(x => x.Category == category.Value);
        return await q.OrderBy(x => x.Category).ThenBy(x => x.SortOrder).ThenBy(x => x.Code)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsCodeAsync(InvoiceLookupCategory category, string code,
        CancellationToken cancellationToken = default)
        => context.InvoiceLookupOptions.AnyAsync(
            x => x.Category == category && x.Code.ToLower() == code.ToLower(),
            cancellationToken);

    public async Task AddAsync(InvoiceLookupOption entity, CancellationToken cancellationToken = default)
    {
        context.InvoiceLookupOptions.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
