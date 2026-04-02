namespace Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

public interface IEmployeeGroupRepository
{
    Task<EmployeeGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EmployeeGroup>> GetByIdsAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken = default);
    Task<EmployeeGroup?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EmployeeGroup>> GetAllAsync(Guid? companyId, string? search, CancellationToken cancellationToken = default);
    Task<EmployeeGroup> AddAsync(EmployeeGroup entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeeGroup entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
