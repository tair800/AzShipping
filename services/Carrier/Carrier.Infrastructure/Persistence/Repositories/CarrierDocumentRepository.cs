using Carrier.Domain.AggregatesModel.CarrierAggregate;
using Microsoft.EntityFrameworkCore;

namespace Carrier.Infrastructure.Persistence.Repositories;

public class CarrierDocumentRepository(CarrierDbContext context) : ICarrierDocumentRepository
{
    public async Task<CarrierDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.CarrierDocuments.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<IReadOnlyList<CarrierDocument>> GetByCarrierIdAsync(Guid carrierId, CancellationToken cancellationToken = default)
        => await context.CarrierDocuments
            .Where(d => d.CarrierId == carrierId)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync(cancellationToken);

    public async Task<CarrierDocument> AddAsync(CarrierDocument entity, CancellationToken cancellationToken = default)
    {
        await context.CarrierDocuments.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(CarrierDocument entity, CancellationToken cancellationToken = default)
    {
        context.CarrierDocuments.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.CarrierDocuments.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            context.CarrierDocuments.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
