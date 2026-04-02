using MediatR;
using Settings.Application.DTOs.QuoteSource;

namespace Settings.Application.Features.QuoteSources.Queries.GetById;

public sealed record GetQuoteSourceByIdQuery(Guid Id) : IRequest<QuoteSourceDto?>;
