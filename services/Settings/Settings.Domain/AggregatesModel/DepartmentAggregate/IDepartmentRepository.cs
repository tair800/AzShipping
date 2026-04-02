namespace Settings.Domain.AggregatesModel.DepartmentAggregate;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Department>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Department> AddAsync(Department entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Department entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
