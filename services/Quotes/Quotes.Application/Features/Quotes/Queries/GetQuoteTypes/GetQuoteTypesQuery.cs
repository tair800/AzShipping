using MediatR;
using Quotes.Application.DTOs.Quote;

namespace Quotes.Application.Features.Quotes.Queries.GetQuoteTypes;

public sealed record GetQuoteTypesQuery(bool IncludeInactive = false) : IRequest<IReadOnlyList<QuoteTypeDto>>;
