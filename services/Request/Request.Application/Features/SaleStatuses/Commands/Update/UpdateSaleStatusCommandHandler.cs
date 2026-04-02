using MediatR;
using Request.Application.DTOs.SaleStatus;
using Request.Application.Features.SaleStatuses;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Application.Features.SaleStatuses.Commands.Update;

public sealed class UpdateSaleStatusCommandHandler(ISaleStatusRepository repository) : IRequestHandler<UpdateSaleStatusCommand, SaleStatusDto?>
{
    public async Task<SaleStatusDto?> Handle(UpdateSaleStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.SortOrder = request.Dto.SortOrder;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return SaleStatusMapper.MapToDto(entity);
    }
}
