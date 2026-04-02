using MediatR;
using Request.Application.DTOs.RequestComment;
using Request.Application.Features.RequestComments;
using Request.Domain.AggregatesModel.RequestCommentAggregate;

namespace Request.Application.Features.RequestComments.Queries.GetByRequestId;

public sealed class GetRequestCommentsByRequestIdQueryHandler(IRequestCommentRepository repository)
    : IRequestHandler<GetRequestCommentsByRequestIdQuery, IReadOnlyList<RequestCommentDto>>
{
    public async Task<IReadOnlyList<RequestCommentDto>> Handle(GetRequestCommentsByRequestIdQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByRequestIdAsync(request.RequestId, cancellationToken);
        return list.Select(RequestCommentMapper.MapToDto).ToList();
    }
}
