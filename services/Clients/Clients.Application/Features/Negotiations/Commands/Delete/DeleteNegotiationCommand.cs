using MediatR;

namespace Clients.Application.Features.Negotiations.Commands.Delete;

public sealed record DeleteNegotiationCommand(Guid Id) : IRequest<bool>;
