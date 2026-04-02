using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Domain.AggregatesModel.CountryAggregate;

public interface ICountryRepository
{
    Task<Country?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Country>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default);
    Task<Country> AddAsync(Country entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Country entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task SetGlobalZonesAsync(Guid countryId, IEnumerable<Guid> globalZoneIds, CancellationToken cancellationToken = default);
}
