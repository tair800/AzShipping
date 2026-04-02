using MediatR;
using Request.Application.DTOs.RequestComment;

namespace Request.Application.Features.RequestComments.Queries.GetByRequestId;

public sealed record GetRequestCommentsByRequestIdQuery(Guid RequestId) : IRequest<IReadOnlyList<RequestCommentDto>>;
