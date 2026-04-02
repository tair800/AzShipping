using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;

namespace Settings.Application.Features.SalesFunnelStatuses.Commands.Update;

public sealed class UpdateSalesFunnelStatusCommandHandler(ISalesFunnelStatusRepository repository) : IRequestHandler<UpdateSalesFunnelStatusCommand, SalesFunnelStatusDto?>
{
    public async Task<SalesFunnelStatusDto?> Handle(UpdateSalesFunnelStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.StatusPosition = request.Dto.StatusPosition;
        entity.ResponsibleManagerId = request.Dto.ResponsibleManagerId;
        entity.NumberOfDays = request.Dto.NumberOfDays;
        entity.SendToEmail = request.Dto.SendToEmail;
        entity.SendNotification = request.Dto.SendNotification;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return new SalesFunnelStatusDto { Id = entity.Id, Name = entity.Name, StatusPosition = entity.StatusPosition, ResponsibleManagerId = entity.ResponsibleManagerId, NumberOfDays = entity.NumberOfDays, SendToEmail = entity.SendToEmail, SendNotification = entity.SendNotification, IsActive = entity.IsActive, CreatedAt = entity.CreatedAt, UpdatedAt = entity.UpdatedAt };
    }
}
