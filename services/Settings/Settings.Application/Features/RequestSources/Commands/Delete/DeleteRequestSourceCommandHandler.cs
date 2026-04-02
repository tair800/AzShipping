using MediatR;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;

namespace Settings.Application.Features.RequestSources.Commands.Delete;

public sealed class DeleteRequestSourceCommandHandler(IRequestSourceRepository repository) : IRequestHandler<DeleteRequestSourceCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
