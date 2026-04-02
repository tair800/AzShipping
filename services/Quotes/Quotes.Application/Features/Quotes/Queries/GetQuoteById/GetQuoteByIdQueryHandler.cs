using MediatR;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Features.Quotes;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Application.Features.Quotes.Queries.GetQuoteById;

public sealed class GetQuoteByIdQueryHandler(
    IQuoteRepository repository,
    IQuoteTypeRepository typeRepository) : IRequestHandler<GetQuoteByIdQuery, QuoteDto?>
{
    public async Task<QuoteDto?> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var quoteType = await typeRepository.GetByIdAsync(entity.QuoteTypeId, cancellationToken);
        return QuoteMapper.MapToDto(entity, quoteType);
    }
}
