namespace Settings.Domain.AggregatesModel.ClientSourceAggregate;

public interface IClientSourceRepository
{
    Task<ClientSource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClientSource>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ClientSource> AddAsync(ClientSource entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ClientSource entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
