using Identity.Application.DTOs.User;

namespace Identity.Application.Interfaces.Services;

public interface IUserDtoEnrichmentService
{
    Task<UserDto> EnrichAsync(UserDto dto, CancellationToken cancellationToken);

    Task<IReadOnlyList<UserDto>> EnrichAsync(IReadOnlyCollection<UserDto> users, CancellationToken cancellationToken);
}
