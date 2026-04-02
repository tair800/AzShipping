using MediatR;
using Request.Domain.AggregatesModel.SaleAggregate;

namespace Request.Application.Features.Sales.Commands.Delete;

public sealed class DeleteSaleCommandHandler(ISaleRepository repository) : IRequestHandler<DeleteSaleCommand, bool>
{
    public async Task<bool> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
