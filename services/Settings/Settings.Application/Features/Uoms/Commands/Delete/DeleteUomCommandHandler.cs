using MediatR;
using Settings.Domain.AggregatesModel.UomAggregate;

namespace Settings.Application.Features.Uoms.Commands.Delete;

public sealed class DeleteUomCommandHandler(IUomRepository repository) : IRequestHandler<DeleteUomCommand, bool>
{
    public async Task<bool> Handle(DeleteUomCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
