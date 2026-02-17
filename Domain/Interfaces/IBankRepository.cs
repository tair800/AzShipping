using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IBankRepository
{
    Task<IEnumerable<Bank>> GetAllAsync();
    Task<Bank?> GetByIdAsync(Guid id);
    Task<Bank> CreateAsync(Bank bank);
    Task<Bank> UpdateAsync(Bank bank);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

