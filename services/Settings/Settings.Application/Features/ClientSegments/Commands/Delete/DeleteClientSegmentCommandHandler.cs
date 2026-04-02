using MediatR;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;

namespace Settings.Application.Features.ClientSegments.Commands.Delete;

public sealed class DeleteClientSegmentCommandHandler(IClientSegmentRepository repository) : IRequestHandler<DeleteClientSegmentCommand, bool>
{
    public async Task<bool> Handle(DeleteClientSegmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
