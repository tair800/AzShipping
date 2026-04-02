using General.Domain.AggregatesModel.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class EmployeeRepository(GeneralDbContext context) : IEmployeeRepository
{
    public async System.Threading.Tasks.Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async System.Threading.Tasks.Task<Employee?> GetTrackedByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

    public async System.Threading.Tasks.Task<Employee?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        => await context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.UserId == userId, cancellationToken);

    public async System.Threading.Tasks.Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Employees.AsNoTracking().OrderBy(e => e.FullName).ThenBy(e => e.Username).ToListAsync(cancellationToken);

    public async System.Threading.Tasks.Task<bool> ExistsByUserIdAsync(long userId, Guid? excludeEmployeeId, CancellationToken cancellationToken = default)
    {
        var q = context.Employees.Where(e => e.UserId == userId);
        if (excludeEmployeeId.HasValue)
            q = q.Where(e => e.Id != excludeEmployeeId.Value);
        return await q.AnyAsync(cancellationToken);
    }

    public async System.Threading.Tasks.Task<Employee> AddAsync(Employee entity, CancellationToken cancellationToken = default)
    {
        context.Employees.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(Employee entity, CancellationToken cancellationToken = default)
    {
        context.Employees.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Employees.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.Employees.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
