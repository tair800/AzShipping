using Identity.Application.DTOs.User;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommand : IRequest<UserDto>, ITransactionalRequest
{
    public CreateUserDto CreateUserDto { get; init; }

    public CreateUserCommand(CreateUserDto createUserDto)
    {
        CreateUserDto = createUserDto ?? throw new ArgumentNullException(nameof(createUserDto));
    }
}