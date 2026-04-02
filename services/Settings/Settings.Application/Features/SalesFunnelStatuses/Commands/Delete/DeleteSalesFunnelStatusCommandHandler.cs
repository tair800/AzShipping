using MediatR;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;

namespace Settings.Application.Features.SalesFunnelStatuses.Commands.Delete;

public sealed class DeleteSalesFunnelStatusCommandHandler(ISalesFunnelStatusRepository repository) : IRequestHandler<DeleteSalesFunnelStatusCommand, bool>
{
    public async Task<bool> Handle(DeleteSalesFunnelStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
