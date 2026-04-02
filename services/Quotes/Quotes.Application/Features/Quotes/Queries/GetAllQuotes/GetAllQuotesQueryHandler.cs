using MediatR;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Features.Quotes;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Application.Features.Quotes.Queries.GetAllQuotes;

public sealed class GetAllQuotesQueryHandler(
    IQuoteRepository repository,
    IQuoteTypeRepository typeRepository) : IRequestHandler<GetAllQuotesQuery, IReadOnlyList<QuoteDto>>
{
    public async Task<IReadOnlyList<QuoteDto>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Mode, request.Direction, request.SubType, cancellationToken);
        var typeIds = entities.Select(x => x.QuoteTypeId).Distinct().ToList();
        var types = (await typeRepository.GetAllAsync(cancellationToken)).Where(t => typeIds.Contains(t.Id)).ToList();
        return entities.Select(e =>
        {
            var qt = types.FirstOrDefault(t => t.Id == e.QuoteTypeId);
            return QuoteMapper.MapToDto(e, qt);
        }).ToList();
    }
}
