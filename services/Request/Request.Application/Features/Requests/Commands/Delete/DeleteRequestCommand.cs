using MediatR;

namespace Request.Application.Features.Requests.Commands.Delete;

public sealed record DeleteRequestCommand(Guid Id) : IRequest<bool>;
