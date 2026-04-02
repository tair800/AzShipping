using MediatR;
using Quotes.Application.DTOs.Quote;

namespace Quotes.Application.Features.Quotes.Commands.CreateQuote;

public sealed record CreateQuoteCommand(CreateOrUpdateQuoteDto Dto) : IRequest<QuoteDto>;
