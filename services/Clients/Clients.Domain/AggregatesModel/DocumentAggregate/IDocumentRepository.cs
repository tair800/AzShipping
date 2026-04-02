namespace Clients.Domain.AggregatesModel.DocumentAggregate;

public interface IDocumentRepository
{
    Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Document>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default);
    Task<Document> AddAsync(Document entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Document entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
