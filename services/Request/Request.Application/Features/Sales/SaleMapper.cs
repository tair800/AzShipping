using Request.Application.DTOs.Sale;
using SaleEntity = Request.Domain.AggregatesModel.SaleAggregate.Sale;

namespace Request.Application.Features.Sales;

public static class SaleMapper
{
    public static SaleDto MapToDto(SaleEntity? entity)
    {
        if (entity == null) return null!;
        return new SaleDto(
            entity.Id,
            entity.CreationDate,
            entity.RequestNumber,
            entity.HasSea,
            entity.HasAir,
            entity.HasRail,
            entity.HasRoad,
            entity.ClientId,
            entity.ClientName,
            entity.SubType,
            entity.CarrierId,
            entity.CarrierName,
            entity.SaleStatusId,
            entity.SaleStatus?.Name,
            entity.StartDate,
            entity.ExpirationDate,
            entity.CargoName,
            entity.CargoVolume,
            entity.CargoWeight,
            entity.CargoSize,
            entity.LoadingPlace,
            entity.UnloadingPlace,
            entity.DealValue,
            entity.DealValueCurrency,
            entity.ManagerSellerName,
            entity.PriceProposal,
            entity.SaleListStatus,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt);
    }
}
