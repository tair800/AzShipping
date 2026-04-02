using Accounting.Domain.AggregatesModel.OperationActAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationActs.Commands.Delete;

public sealed class DeleteOperationActCommandHandler(IOperationActRepository repo)
    : IRequestHandler<DeleteOperationActCommand, bool>
{
    public async Task<bool> Handle(DeleteOperationActCommand request, CancellationToken cancellationToken)
    {
        var entity = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return false;
        await repo.DeleteAsync(entity, cancellationToken);
        return true;
    }
}
