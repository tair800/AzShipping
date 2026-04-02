namespace General.Domain.AggregatesModel.EmployeeAggregate;

public interface IEmployeeNoteRepository
{
    System.Threading.Tasks.Task<IReadOnlyList<EmployeeNote>> ListByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<EmployeeNote> AddAsync(EmployeeNote entity, CancellationToken cancellationToken = default);
}
