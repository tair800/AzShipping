namespace Settings.Domain.AggregatesModel.PricingTypeAggregate;

public interface IPricingTypeRepository
{
    Task<PricingType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PricingType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PricingType> AddAsync(PricingType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PricingType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
