using MediatR;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Application.Features.SaleStatuses.Commands.Delete;

public sealed class DeleteSaleStatusCommandHandler(ISaleStatusRepository repository) : IRequestHandler<DeleteSaleStatusCommand, bool>
{
    public async Task<bool> Handle(DeleteSaleStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
