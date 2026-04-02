using MediatR;
using Request.Application.DTOs.RequestComment;

namespace Request.Application.Features.RequestComments.Queries.GetById;

public sealed record GetRequestCommentByIdQuery(Guid Id) : IRequest<RequestCommentDto?>;
