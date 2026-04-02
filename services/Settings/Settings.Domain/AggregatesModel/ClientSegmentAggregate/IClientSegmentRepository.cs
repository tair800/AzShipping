namespace Settings.Domain.AggregatesModel.ClientSegmentAggregate;

public interface IClientSegmentRepository
{
    Task<ClientSegment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientSegment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ClientSegment> AddAsync(ClientSegment entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ClientSegment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
