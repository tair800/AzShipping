using MediatR;
using Quotes.Application.DTOs.Quote;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Application.Features.Quotes.Queries.GetQuoteFunnel;

public sealed class GetQuoteFunnelQueryHandler(IQuoteRepository repository)
    : IRequestHandler<GetQuoteFunnelQuery, QuoteFunnelSummaryDto>
{
    private const string NoStageKey = "\u0001no-stage\u0001";

    public async Task<QuoteFunnelSummaryDto> Handle(GetQuoteFunnelQuery request, CancellationToken cancellationToken)
    {
        var entities = request.ApplyFilters
            ? await repository.GetAllAsync(request.Mode, request.Direction, request.SubType, cancellationToken)
            : await repository.GetAllAsync(null, null, null, cancellationToken);

        var groups = entities
            .GroupBy(q =>
                string.IsNullOrWhiteSpace(q.QuoteStatus)
                    ? NoStageKey
                    : q.QuoteStatus.Trim().ToLowerInvariant())
            .Select(g =>
            {
                var matchingKey = g.Key == NoStageKey ? "" : g.Key;
                var stageName = g.Key == NoStageKey
                    ? "(No stage)"
                    : g
                        .Where(x => !string.IsNullOrWhiteSpace(x.QuoteStatus))
                        .Select(x => x.QuoteStatus!.Trim())
                        .GroupBy(x => x, StringComparer.Ordinal)
                        .OrderByDescending(x => x.Count())
                        .First()
                        .Key;

                return new QuoteFunnelBucketDto(
                    stageName,
                    matchingKey,
                    g.Count(),
                    g.Sum(x => x.PriceStandard ?? 0));
            })
            .OrderBy(b => b.MatchingKey == "" ? 1 : 0)
            .ThenBy(b => b.StageName, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var total = entities.Count;
        return new QuoteFunnelSummaryDto(groups, total);
    }
}
