namespace Clients.Domain.AggregatesModel.ClientAggregate;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Client> AddAsync(Client entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Client entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
