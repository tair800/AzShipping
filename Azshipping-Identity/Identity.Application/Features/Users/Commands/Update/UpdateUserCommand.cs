using MediatR;
using Identity.Application.DTOs.User;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Users.Commands.Update;

public sealed record UpdateUserCommand : IRequest<UserDto?>, ITransactionalRequest
{
    public UpdateUserDto UpdateUserDto { get; init; }

    public UpdateUserCommand(UpdateUserDto updateUserDto)
    {
        UpdateUserDto = updateUserDto ?? throw new ArgumentNullException(nameof(updateUserDto));
    }
}
