using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Roles.Commands.Delete;

public sealed record DeleteRoleCommand(long Id) : IRequest<bool>, ITransactionalRequest;