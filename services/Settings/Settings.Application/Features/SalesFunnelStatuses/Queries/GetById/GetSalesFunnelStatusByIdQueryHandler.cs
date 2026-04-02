using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;

namespace Settings.Application.Features.SalesFunnelStatuses.Queries.GetById;

public sealed class GetSalesFunnelStatusByIdQueryHandler(ISalesFunnelStatusRepository repository) : IRequestHandler<GetSalesFunnelStatusByIdQuery, SalesFunnelStatusDto?>
{
    public async Task<SalesFunnelStatusDto?> Handle(GetSalesFunnelStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (e == null) return null;
        return new SalesFunnelStatusDto { Id = e.Id, Name = e.Name, StatusPosition = e.StatusPosition, ResponsibleManagerId = e.ResponsibleManagerId, NumberOfDays = e.NumberOfDays, SendToEmail = e.SendToEmail, SendNotification = e.SendNotification, IsActive = e.IsActive, CreatedAt = e.CreatedAt, UpdatedAt = e.UpdatedAt };
    }
}
