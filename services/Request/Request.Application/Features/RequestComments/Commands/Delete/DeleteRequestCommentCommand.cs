using MediatR;

namespace Request.Application.Features.RequestComments.Commands.Delete;

public sealed record DeleteRequestCommentCommand(Guid Id) : IRequest<bool>;
