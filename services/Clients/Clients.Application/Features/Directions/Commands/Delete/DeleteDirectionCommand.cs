using MediatR;

namespace Clients.Application.Features.Directions.Commands.Delete;

public sealed record DeleteDirectionCommand(Guid Id) : IRequest<bool>;
