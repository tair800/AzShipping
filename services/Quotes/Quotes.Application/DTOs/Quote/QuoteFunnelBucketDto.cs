namespace Quotes.Application.DTOs.Quote;

/// <param name="MatchingKey">Trimmed, lower-cased <c>QuoteStatus</c>; pairs with Quote Source name case-insensitively.</param>
public sealed record QuoteFunnelBucketDto(string StageName, string MatchingKey, int Count, decimal SumPriceStandard);

public sealed record QuoteFunnelSummaryDto(IReadOnlyList<QuoteFunnelBucketDto> Buckets, int TotalQuotes);
