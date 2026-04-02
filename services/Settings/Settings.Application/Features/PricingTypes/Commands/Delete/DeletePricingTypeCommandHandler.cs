using MediatR;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;

namespace Settings.Application.Features.PricingTypes.Commands.Delete;

public sealed class DeletePricingTypeCommandHandler(IPricingTypeRepository repository) : IRequestHandler<DeletePricingTypeCommand, bool>
{
    public async Task<bool> Handle(DeletePricingTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
