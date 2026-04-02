using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public sealed class EmployeeGroupRepository(SettingsDbContext context) : IEmployeeGroupRepository
{
    public Task<EmployeeGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => context.EmployeeGroups.Include(e => e.Company).AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async Task<IReadOnlyList<EmployeeGroup>> GetByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken = default)
    {
        if (ids == null || ids.Count == 0) return [];
        return await context.EmployeeGroups.AsNoTracking()
            .Include(e => e.Company)
            .Where(e => ids.Contains(e.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<EmployeeGroup>> GetAllAsync(Guid? companyId, string? search, CancellationToken cancellationToken = default)
    {
        var q = context.EmployeeGroups.AsNoTracking().Include(e => e.Company).AsQueryable();
        if (companyId.HasValue)
            q = q.Where(e => e.CompanyId == companyId);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim();
            q = q.Where(e => EF.Functions.ILike(e.Name, $"%{s}%"));
        }

        return await q.OrderBy(e => e.Name).ToListAsync(cancellationToken);
    }

    public async Task<EmployeeGroup> AddAsync(EmployeeGroup entity, CancellationToken cancellationToken = default)
    {
        context.EmployeeGroups.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(EmployeeGroup entity, CancellationToken cancellationToken = default)
    {
        context.EmployeeGroups.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.EmployeeGroups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (e != null)
        {
            context.EmployeeGroups.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public Task<EmployeeGroup?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default)
        => context.EmployeeGroups.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
}
