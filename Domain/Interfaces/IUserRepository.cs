using AzShipping.Domain.Entities;

namespace AzShipping.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>> GetActiveUsersAsync();
}

