namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Project>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default);
    Task<Project> AddAsync(Project entity, CancellationToken cancellationToken = default);
}
