using MediatR;
using Quotes.Application.DTOs.Quote;

namespace Quotes.Application.Features.Quotes.Queries.GetQuoteFunnel;

/// <param name="ApplyFilters">When false (default), all quotes are included so funnel matches list totals. When true, same filters as quote list.</param>
public sealed record GetQuoteFunnelQuery(
    string? Mode = null,
    string? Direction = null,
    string? SubType = null,
    bool ApplyFilters = false)
    : IRequest<QuoteFunnelSummaryDto>;
