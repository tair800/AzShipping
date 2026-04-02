using MediatR;
using Request.Application.DTOs.RequestComment;
using Request.Application.Features.RequestComments;
using Request.Domain.AggregatesModel.RequestCommentAggregate;

namespace Request.Application.Features.RequestComments.Commands.Update;

public sealed class UpdateRequestCommentCommandHandler(IRequestCommentRepository repository)
    : IRequestHandler<UpdateRequestCommentCommand, RequestCommentDto?>
{
    public async Task<RequestCommentDto?> Handle(UpdateRequestCommentCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;
        existing.Comments = request.Dto.Comments ?? existing.Comments;
        await repository.UpdateAsync(existing, cancellationToken);
        var loaded = await repository.GetByIdAsync(request.Id, cancellationToken);
        return RequestCommentMapper.MapToDto(loaded ?? existing);
    }
}
