using MediatR;

namespace Clients.Application.Features.Clients.Commands.Delete;

public sealed record DeleteClientCommand(Guid Id) : IRequest<bool>;
