using MediatR;

namespace Request.Application.Features.RequestNegotiations.Commands.Delete;

public sealed record DeleteRequestNegotiationCommand(Guid Id) : IRequest<bool>;
