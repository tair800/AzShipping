using MediatR;

namespace Settings.Application.Features.QuoteSources.Commands.Delete;

public sealed record DeleteQuoteSourceCommand(Guid Id) : IRequest<bool>;
