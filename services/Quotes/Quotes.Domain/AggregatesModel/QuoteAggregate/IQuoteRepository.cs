namespace Quotes.Domain.AggregatesModel.QuoteAggregate;

public interface IQuoteRepository
{
    Task<QuoteEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<QuoteEntity>> GetAllAsync(string? mode = null, string? direction = null, string? subType = null, CancellationToken cancellationToken = default);
    Task<int> GetNextSequenceForPrefixAsync(string prefix, CancellationToken cancellationToken = default);
    Task<QuoteEntity> AddAsync(QuoteEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(QuoteEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
