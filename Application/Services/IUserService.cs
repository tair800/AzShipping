using AzShipping.Application.DTOs;

namespace AzShipping.Application.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<IEnumerable<UserDto>> GetActiveUsersAsync();
    Task<UserDto?> GetByIdAsync(Guid id);
}

