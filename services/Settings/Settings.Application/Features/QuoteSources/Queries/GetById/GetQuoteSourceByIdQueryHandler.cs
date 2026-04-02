using MediatR;
using Settings.Application.DTOs.QuoteSource;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;

namespace Settings.Application.Features.QuoteSources.Queries.GetById;

public sealed class GetQuoteSourceByIdQueryHandler(IQuoteSourceRepository repository) : IRequestHandler<GetQuoteSourceByIdQuery, QuoteSourceDto?>
{
    public async Task<QuoteSourceDto?> Handle(GetQuoteSourceByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : new QuoteSourceDto(e.Id, e.Name, e.DisplayOrder, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}
