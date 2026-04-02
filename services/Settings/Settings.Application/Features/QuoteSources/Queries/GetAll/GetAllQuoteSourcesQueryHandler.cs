using MediatR;
using Settings.Application.DTOs.QuoteSource;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;

namespace Settings.Application.Features.QuoteSources.Queries.GetAll;

public sealed class GetAllQuoteSourcesQueryHandler(IQuoteSourceRepository repository) : IRequestHandler<GetAllQuoteSourcesQuery, IReadOnlyList<QuoteSourceDto>>
{
    public async Task<IReadOnlyList<QuoteSourceDto>> Handle(GetAllQuoteSourcesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(e => new QuoteSourceDto(e.Id, e.Name, e.DisplayOrder, e.IsActive, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}
