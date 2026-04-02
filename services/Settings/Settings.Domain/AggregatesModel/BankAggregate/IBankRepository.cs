namespace Settings.Domain.AggregatesModel.BankAggregate;

public interface IBankRepository
{
    Task<Bank?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Bank>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Bank> AddAsync(Bank entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Bank entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
