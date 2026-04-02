namespace Settings.Domain.AggregatesModel.DrivingLicenceCategoryAggregate;

public interface IDrivingLicenceCategoryRepository
{
    Task<DrivingLicenceCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DrivingLicenceCategory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DrivingLicenceCategory> AddAsync(DrivingLicenceCategory entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(DrivingLicenceCategory entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
