namespace Settings.Domain.AggregatesModel.QuoteSourceAggregate;

public interface IQuoteSourceRepository
{
    Task<QuoteSource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<QuoteSource>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<QuoteSource> AddAsync(QuoteSource entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(QuoteSource entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
