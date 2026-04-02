using MediatR;
using Settings.Application.DTOs.QuoteSource;

namespace Settings.Application.Features.QuoteSources.Queries.GetAll;

public sealed record GetAllQuoteSourcesQuery : IRequest<IReadOnlyList<QuoteSourceDto>>;
