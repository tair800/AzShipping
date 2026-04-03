using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Users.Commands.Delete;

public sealed record DeleteUserCommand(long Id) : IRequest<bool>, ITransactionalRequest;
