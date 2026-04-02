using MediatR;
using Request.Application.DTOs.RequestComment;
using Request.Application.Features.RequestComments;
using Request.Domain.AggregatesModel.RequestCommentAggregate;

namespace Request.Application.Features.RequestComments.Commands.Create;

public sealed class CreateRequestCommentCommandHandler(IRequestCommentRepository repository)
    : IRequestHandler<CreateRequestCommentCommand, RequestCommentDto>
{
    public async Task<RequestCommentDto> Handle(CreateRequestCommentCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var entity = new RequestComment
        {
            Id = Guid.NewGuid(),
            RequestId = d.RequestId,
            Comments = d.Comments,
            Date = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return RequestCommentMapper.MapToDto(loaded ?? entity);
    }
}
