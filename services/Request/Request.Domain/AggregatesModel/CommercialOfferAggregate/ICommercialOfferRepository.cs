namespace Request.Domain.AggregatesModel.CommercialOfferAggregate;

public interface ICommercialOfferRepository
{
    Task<CommercialOffer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CommercialOffer>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);
    Task<CommercialOffer> AddAsync(CommercialOffer entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CommercialOffer entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
