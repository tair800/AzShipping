using MediatR;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Features.Quotes;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Application.Features.Quotes.Queries.GetQuoteTypes;

public sealed class GetQuoteTypesQueryHandler(IQuoteTypeRepository repository)
    : IRequestHandler<GetQuoteTypesQuery, IReadOnlyList<QuoteTypeDto>>
{
    public async Task<IReadOnlyList<QuoteTypeDto>> Handle(GetQuoteTypesQuery request, CancellationToken cancellationToken)
    {
        var types = request.IncludeInactive
            ? await repository.GetAllAsync(cancellationToken)
            : await repository.GetAllActiveAsync(cancellationToken);
        return types.Select(QuoteMapper.MapTypeToDto).ToList();
    }
}
