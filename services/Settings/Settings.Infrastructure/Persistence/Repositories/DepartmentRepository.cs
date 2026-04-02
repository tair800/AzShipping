using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.DepartmentAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class DepartmentRepository(SettingsDbContext context) : IDepartmentRepository
{
    public async Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Departments.Include(d => d.Company).FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Departments.Include(d => d.Company).OrderBy(d => d.Name).ToListAsync(cancellationToken);

    public async Task<Department> AddAsync(Department entity, CancellationToken cancellationToken = default)
    {
        context.Departments.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Department entity, CancellationToken cancellationToken = default)
    {
        context.Departments.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Departments.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Departments.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
