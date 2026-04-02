using MediatR;
using Settings.Application.DTOs.QuoteSource;

namespace Settings.Application.Features.QuoteSources.Commands.Update;

public sealed record UpdateQuoteSourceCommand(Guid Id, UpdateQuoteSourceDto Dto) : IRequest<QuoteSourceDto?>;
