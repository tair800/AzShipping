using MediatR;
using Request.Application.DTOs.RequestComment;

namespace Request.Application.Features.RequestComments.Commands.Update;

public sealed record UpdateRequestCommentCommand(Guid Id, UpdateRequestCommentDto Dto) : IRequest<RequestCommentDto?>;
