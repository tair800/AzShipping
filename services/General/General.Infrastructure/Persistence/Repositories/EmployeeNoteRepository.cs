using General.Domain.AggregatesModel.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class EmployeeNoteRepository(GeneralDbContext context) : IEmployeeNoteRepository
{
    public async System.Threading.Tasks.Task<IReadOnlyList<EmployeeNote>> ListByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
        => await context.EmployeeNotes.AsNoTracking()
            .Where(n => n.EmployeeId == employeeId)
            .OrderByDescending(n => n.CreatedAtUtc)
            .ToListAsync(cancellationToken);

    public async System.Threading.Tasks.Task<EmployeeNote> AddAsync(EmployeeNote entity, CancellationToken cancellationToken = default)
    {
        context.EmployeeNotes.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
