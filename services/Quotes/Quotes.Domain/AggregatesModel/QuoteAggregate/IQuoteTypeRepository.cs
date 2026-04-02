namespace Quotes.Domain.AggregatesModel.QuoteAggregate;

public interface IQuoteTypeRepository
{
    Task<QuoteType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<QuoteType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<QuoteType>> GetAllActiveAsync(CancellationToken cancellationToken = default);
}
