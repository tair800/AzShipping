using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Domain.AggregatesModel.CityAggregate;

public interface ICityRepository
{
    Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<City>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<City>> GetByStatusAsync(EntityStatus status, CancellationToken cancellationToken = default);
    Task<City> AddAsync(City entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(City entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
