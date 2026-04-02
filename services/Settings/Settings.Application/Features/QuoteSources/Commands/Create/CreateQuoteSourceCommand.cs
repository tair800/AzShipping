using MediatR;
using Settings.Application.DTOs.QuoteSource;

namespace Settings.Application.Features.QuoteSources.Commands.Create;

public sealed record CreateQuoteSourceCommand(CreateQuoteSourceDto Dto) : IRequest<QuoteSourceDto>;
