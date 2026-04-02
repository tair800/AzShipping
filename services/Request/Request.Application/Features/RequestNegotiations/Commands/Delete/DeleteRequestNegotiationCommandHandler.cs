using MediatR;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;

namespace Request.Application.Features.RequestNegotiations.Commands.Delete;

public sealed class DeleteRequestNegotiationCommandHandler(IRequestNegotiationRepository repository) : IRequestHandler<DeleteRequestNegotiationCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestNegotiationCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
