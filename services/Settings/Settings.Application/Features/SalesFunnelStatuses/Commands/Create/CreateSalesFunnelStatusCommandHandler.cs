using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;

namespace Settings.Application.Features.SalesFunnelStatuses.Commands.Create;

public sealed class CreateSalesFunnelStatusCommandHandler(ISalesFunnelStatusRepository repository) : IRequestHandler<CreateSalesFunnelStatusCommand, SalesFunnelStatusDto>
{
    public async Task<SalesFunnelStatusDto> Handle(CreateSalesFunnelStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = new SalesFunnelStatus
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            StatusPosition = request.Dto.StatusPosition,
            ResponsibleManagerId = request.Dto.ResponsibleManagerId,
            NumberOfDays = request.Dto.NumberOfDays,
            SendToEmail = request.Dto.SendToEmail,
            SendNotification = request.Dto.SendNotification,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return new SalesFunnelStatusDto { Id = entity.Id, Name = entity.Name, StatusPosition = entity.StatusPosition, ResponsibleManagerId = entity.ResponsibleManagerId, NumberOfDays = entity.NumberOfDays, SendToEmail = entity.SendToEmail, SendNotification = entity.SendNotification, IsActive = entity.IsActive, CreatedAt = entity.CreatedAt, UpdatedAt = null };
    }
}
