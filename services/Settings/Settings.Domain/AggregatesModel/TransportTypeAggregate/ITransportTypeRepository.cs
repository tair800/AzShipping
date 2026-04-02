namespace Settings.Domain.AggregatesModel.TransportTypeAggregate;

public interface ITransportTypeRepository
{
    Task<TransportType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TransportType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TransportType> AddAsync(TransportType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TransportType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
