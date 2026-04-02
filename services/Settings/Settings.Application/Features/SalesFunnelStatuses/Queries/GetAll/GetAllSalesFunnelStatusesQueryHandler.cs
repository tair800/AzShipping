using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;

namespace Settings.Application.Features.SalesFunnelStatuses.Queries.GetAll;

public sealed class GetAllSalesFunnelStatusesQueryHandler(ISalesFunnelStatusRepository repository) : IRequestHandler<GetAllSalesFunnelStatusesQuery, IReadOnlyList<SalesFunnelStatusDto>>
{
    public async Task<IReadOnlyList<SalesFunnelStatusDto>> Handle(GetAllSalesFunnelStatusesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(e => new SalesFunnelStatusDto { Id = e.Id, Name = e.Name, StatusPosition = e.StatusPosition, ResponsibleManagerId = e.ResponsibleManagerId, NumberOfDays = e.NumberOfDays, SendToEmail = e.SendToEmail, SendNotification = e.SendNotification, IsActive = e.IsActive, CreatedAt = e.CreatedAt, UpdatedAt = e.UpdatedAt }).ToList();
    }
}
