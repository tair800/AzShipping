namespace General.Domain.AggregatesModel.EmployeeAggregate;

public interface IEmployeeRepository
{
    System.Threading.Tasks.Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<Employee?> GetTrackedByIdAsync(Guid id, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<Employee?> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<bool> ExistsByUserIdAsync(long userId, Guid? excludeEmployeeId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<Employee> AddAsync(Employee entity, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task UpdateAsync(Employee entity, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
