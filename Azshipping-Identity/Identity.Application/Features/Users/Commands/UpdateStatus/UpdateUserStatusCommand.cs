using Identity.Application.DTOs.User;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Users.Commands.UpdateStatus;

public sealed record UpdateUserStatusCommand(UpdateUserStatusDto Dto) : IRequest<UserDto>, ITransactionalRequest;
