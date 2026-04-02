using MediatR;
using Request.Application.DTOs.Sale;
using Request.Application.Features.Sales;
using Request.Domain.AggregatesModel.SaleAggregate;

namespace Request.Application.Features.Sales.Commands.Update;

public sealed class UpdateSaleCommandHandler(ISaleRepository repository) : IRequestHandler<UpdateSaleCommand, SaleDto?>
{
    public async Task<SaleDto?> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var d = request.Dto;
        entity.RequestNumber = d.RequestNumber;
        entity.HasSea = d.HasSea;
        entity.HasAir = d.HasAir;
        entity.HasRail = d.HasRail;
        entity.HasRoad = d.HasRoad;
        entity.ClientId = d.ClientId;
        entity.ClientName = d.ClientName;
        entity.SubType = d.SubType;
        entity.CarrierId = d.CarrierId;
        entity.CarrierName = d.CarrierName;
        entity.SaleStatusId = d.SaleStatusId;
        entity.StartDate = d.StartDate;
        entity.ExpirationDate = d.ExpirationDate;
        entity.CargoName = d.CargoName;
        entity.CargoVolume = d.CargoVolume;
        entity.CargoWeight = d.CargoWeight;
        entity.CargoSize = d.CargoSize;
        entity.LoadingPlace = d.LoadingPlace;
        entity.UnloadingPlace = d.UnloadingPlace;
        entity.DealValue = d.DealValue;
        entity.DealValueCurrency = d.DealValueCurrency;
        entity.ManagerSellerName = d.ManagerSellerName;
        entity.PriceProposal = d.PriceProposal;
        entity.SaleListStatus = d.SaleListStatus;
        entity.IsActive = d.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return SaleMapper.MapToDto(loaded ?? entity);
    }
}
