using MediatR;

namespace Quotes.Application.Features.Quotes.Commands.DeleteQuote;

public sealed record DeleteQuoteCommand(Guid Id) : IRequest<bool>;
