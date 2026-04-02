using MediatR;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;

namespace Settings.Application.Features.CarrierTypes.Commands.Delete;

public sealed class DeleteCarrierTypeCommandHandler(ICarrierTypeRepository repository) : IRequestHandler<DeleteCarrierTypeCommand, bool>
{
    public async Task<bool> Handle(DeleteCarrierTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
