using MediatR;
using Quotes.Application.DTOs.Quote;

namespace Quotes.Application.Features.Quotes.Queries.GetQuoteById;

public sealed record GetQuoteByIdQuery(Guid Id) : IRequest<QuoteDto?>;
