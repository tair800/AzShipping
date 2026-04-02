using Clients.Domain.AggregatesModel.DocumentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence.Repositories;

public class DocumentRepository(ClientsDbContext context) : IDocumentRepository
{
    public async Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Documents.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<Document>> GetByClientIdAsync(Guid clientId, CancellationToken cancellationToken = default)
        => await context.Documents.Where(d => d.ClientId == clientId).OrderByDescending(d => d.CreatedAt).ToListAsync(cancellationToken);

    public async Task<Document> AddAsync(Document entity, CancellationToken cancellationToken = default)
    {
        context.Documents.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Document entity, CancellationToken cancellationToken = default)
    {
        var entry = context.Entry(entity);
        if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            context.Documents.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Documents.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.Documents.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
