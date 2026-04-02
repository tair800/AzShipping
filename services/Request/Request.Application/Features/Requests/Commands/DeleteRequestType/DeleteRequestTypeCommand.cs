using MediatR;

namespace Request.Application.Features.Requests.Commands.DeleteRequestType;

public sealed record DeleteRequestTypeCommand(Guid Id) : IRequest<bool>;
