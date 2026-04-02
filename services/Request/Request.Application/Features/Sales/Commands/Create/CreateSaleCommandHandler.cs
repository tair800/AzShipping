using MediatR;
using Request.Application.DTOs.Sale;
using Request.Application.Features.Sales;
using Request.Domain.AggregatesModel.SaleAggregate;

namespace Request.Application.Features.Sales.Commands.Create;

public sealed class CreateSaleCommandHandler(ISaleRepository repository) : IRequestHandler<CreateSaleCommand, SaleDto>
{
    public async Task<SaleDto> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var entity = new Sale
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            RequestNumber = d.RequestNumber,
            HasSea = d.HasSea,
            HasAir = d.HasAir,
            HasRail = d.HasRail,
            HasRoad = d.HasRoad,
            ClientId = d.ClientId,
            ClientName = d.ClientName,
            SubType = d.SubType,
            CarrierId = d.CarrierId,
            CarrierName = d.CarrierName,
            SaleStatusId = d.SaleStatusId,
            StartDate = d.StartDate,
            ExpirationDate = d.ExpirationDate,
            CargoName = d.CargoName,
            CargoVolume = d.CargoVolume,
            CargoWeight = d.CargoWeight,
            CargoSize = d.CargoSize,
            LoadingPlace = d.LoadingPlace,
            UnloadingPlace = d.UnloadingPlace,
            DealValue = d.DealValue,
            DealValueCurrency = d.DealValueCurrency,
            ManagerSellerName = d.ManagerSellerName,
            PriceProposal = d.PriceProposal,
            SaleListStatus = d.SaleListStatus,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return SaleMapper.MapToDto(loaded ?? entity);
    }
}
