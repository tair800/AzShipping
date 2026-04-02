using MediatR;
using Request.Domain.AggregatesModel.RequestAggregate;

namespace Request.Application.Features.Requests.Commands.DeleteRequestType;

public sealed class DeleteRequestTypeCommandHandler(IRequestTypeRepository repository)
    : IRequestHandler<DeleteRequestTypeCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
