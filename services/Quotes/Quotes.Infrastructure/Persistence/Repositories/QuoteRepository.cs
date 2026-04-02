using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Infrastructure.Persistence.Repositories;

public class QuoteRepository(QuotesDbContext context) : IQuoteRepository
{
    public async Task<int> GetNextSequenceForPrefixAsync(string prefix, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(prefix)) return 1;
        var quoteNumbers = await context.Quotes
            .Where(q => q.QuoteNumber.StartsWith(prefix))
            .Select(q => q.QuoteNumber)
            .ToListAsync(cancellationToken);
        var max = 0;
        foreach (var qn in quoteNumbers)
        {
            var suffix = qn.Length > prefix.Length ? qn[prefix.Length..] : "";
            var m = Regex.Match(suffix, @"^\d+");
            if (m.Success && int.TryParse(m.Value, out var n) && n > max) max = n;
        }
        return max + 1;
    }

    public async Task<QuoteEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Quotes
            .Include(x => x.PickupAddress)
            .Include(x => x.DeliveryAddress)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<QuoteEntity>> GetAllAsync(string? mode = null, string? direction = null, string? subType = null, CancellationToken cancellationToken = default)
    {
        var query = context.Quotes.AsQueryable().Join(context.QuoteTypes, q => q.QuoteTypeId, t => t.Id, (q, t) => new { Quote = q, Type = t });
        if (!string.IsNullOrWhiteSpace(mode))
        {
            var modeList = mode.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Where(s => !string.IsNullOrEmpty(s)).ToList();
            if (modeList.Count == 1)
                query = query.Where(x => x.Type.Mode == modeList[0]);
            else if (modeList.Count > 1)
                query = query.Where(x => modeList.Contains(x.Type.Mode));
        }
        if (!string.IsNullOrWhiteSpace(direction))
        {
            var d = direction.Trim();
            query = query.Where(x => x.Type.Direction == d);
        }
        if (!string.IsNullOrWhiteSpace(subType))
        {
            var subTypeList = subType.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Where(s => !string.IsNullOrEmpty(s)).ToList();
            if (subTypeList.Count == 1)
                query = query.Where(x => x.Type.SubType == subTypeList[0]);
            else if (subTypeList.Count > 1)
                query = query.Where(x => subTypeList.Contains(x.Type.SubType ?? ""));
        }
        return await query.OrderByDescending(x => x.Quote.CreationDate).Select(x => x.Quote).ToListAsync(cancellationToken);
    }

    public async Task<QuoteEntity> AddAsync(QuoteEntity entity, CancellationToken cancellationToken = default)
    {
        context.Quotes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(QuoteEntity entity, CancellationToken cancellationToken = default)
    {
        context.Quotes.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Quotes.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Quotes.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
