using MediatR;
using Request.Application.DTOs.RequestComment;

namespace Request.Application.Features.RequestComments.Commands.Create;

public sealed record CreateRequestCommentCommand(CreateRequestCommentDto Dto) : IRequest<RequestCommentDto>;
