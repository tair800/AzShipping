using MediatR;
using Request.Application.DTOs.RequestComment;
using Request.Application.Features.RequestComments;
using Request.Domain.AggregatesModel.RequestCommentAggregate;

namespace Request.Application.Features.RequestComments.Queries.GetById;

public sealed class GetRequestCommentByIdQueryHandler(IRequestCommentRepository repository)
    : IRequestHandler<GetRequestCommentByIdQuery, RequestCommentDto?>
{
    public async Task<RequestCommentDto?> Handle(GetRequestCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : RequestCommentMapper.MapToDto(entity);
    }
}
