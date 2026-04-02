using MediatR;
using Quotes.Application.DTOs.Quote;

namespace Quotes.Application.Features.Quotes.Commands.UpdateQuote;

public sealed record UpdateQuoteCommand(Guid Id, CreateOrUpdateQuoteDto Dto) : IRequest<QuoteDto?>;
