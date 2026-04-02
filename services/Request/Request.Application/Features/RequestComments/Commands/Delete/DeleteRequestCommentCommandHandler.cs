using MediatR;
using Request.Domain.AggregatesModel.RequestCommentAggregate;

namespace Request.Application.Features.RequestComments.Commands.Delete;

public sealed class DeleteRequestCommentCommandHandler(IRequestCommentRepository repository)
    : IRequestHandler<DeleteRequestCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestCommentCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
