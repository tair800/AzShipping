using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence.Repositories;

public class VatDefinitionRepository(AccountingDbContext context) : IVatDefinitionRepository
{
    public async Task<VatDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.VatDefinitions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<VatDefinition?> GetByIdTrackedAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.VatDefinitions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<VatDefinition>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.VatDefinitions.AsNoTracking().OrderBy(x => x.Name).ToListAsync(cancellationToken);

    public async Task<VatDefinition> AddAsync(VatDefinition entity, CancellationToken cancellationToken = default)
    {
        context.VatDefinitions.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(VatDefinition entity, CancellationToken cancellationToken = default)
    {
        context.VatDefinitions.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.VatDefinitions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (e != null)
        {
            context.VatDefinitions.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
