using MediatR;
using Quotes.Application.DTOs.Quote;

namespace Quotes.Application.Features.Quotes.Queries.GetAllQuotes;

public sealed record GetAllQuotesQuery(string? Mode = null, string? Direction = null, string? SubType = null) : IRequest<IReadOnlyList<QuoteDto>>;
