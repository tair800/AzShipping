using Accounting.Domain;
using MediatR;
using Request.Application.DTOs.PriceProposal;
using Request.Application.Features.PriceProposals;
using Request.Application.Services;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.PriceProposals.Commands.Create;

public sealed class CreatePriceProposalCommandHandler(
    IPriceProposalRepository repository,
    IVatRateLookupService vatRateLookup) : IRequestHandler<CreatePriceProposalCommand, PriceProposalDto>
{
    public async Task<PriceProposalDto> Handle(CreatePriceProposalCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var now = DateTime.UtcNow;

        var clientPriceWithVat = d.ClientPriceWithVat;
        if (d.ClientPrice.HasValue && d.ClientVatRateId.HasValue)
        {
            var vatPercent = await vatRateLookup.GetVatPercentAsync(d.ClientVatRateId, cancellationToken);
            if (vatPercent.HasValue)
                clientPriceWithVat = VatCalculation.GrossFromNet(d.ClientPrice.Value, vatPercent.Value);
        }

        var carrierRateWithVat = d.CarrierRateWithVat;
        if (d.CarrierRate.HasValue && d.CarrierVatRateId.HasValue)
        {
            var vatPercent = await vatRateLookup.GetVatPercentAsync(d.CarrierVatRateId, cancellationToken);
            if (vatPercent.HasValue)
                carrierRateWithVat = VatCalculation.GrossFromNet(d.CarrierRate.Value, vatPercent.Value);
        }

        var entity = new PriceProposal
        {
            Id = Guid.NewGuid(),
            RequestId = d.RequestId,
            Type = d.Type ?? "Calculation",
            TemplateName = d.TemplateName,
            CarrierId = d.CarrierId,
            CarrierName = d.CarrierName,
            TypeOfService = d.TypeOfService,
            Name = d.Name ?? "",
            ClientPrice = d.ClientPrice,
            ClientPriceWithVat = clientPriceWithVat,
            ClientVatRateId = d.ClientVatRateId,
            ClientVatRateCode = d.ClientVatRateCode,
            ClientCurrencyId = d.ClientCurrencyId,
            ClientCurrencyCode = d.ClientCurrencyCode,
            SeparateLineInInvoice = d.SeparateLineInInvoice,
            CarrierRate = d.CarrierRate,
            CarrierRateWithVat = carrierRateWithVat,
            CarrierVatRateId = d.CarrierVatRateId,
            CarrierVatRateCode = d.CarrierVatRateCode,
            CarrierCurrencyId = d.CarrierCurrencyId,
            CarrierCurrencyCode = d.CarrierCurrencyCode,
            Expense = d.Expense,
            Profit = d.Profit,
            Route = d.Route,
            Comments = d.Comments,
            CreatedAt = now
        };
        if (d.CargoItems != null && d.CargoItems.Count > 0)
        {
            entity.CargoItems = d.CargoItems.Select(c => new PriceProposalCargo
            {
                Id = Guid.NewGuid(),
                PriceProposalId = entity.Id,
                Description = c.Description,
                Quantity = c.Quantity,
                PackageType = c.PackageType,
                IncludeInsurance = c.IncludeInsurance,
                DescriptionOfGoods = c.DescriptionOfGoods
            }).ToList();
        }
        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return PriceProposalMapper.MapToDto(loaded ?? entity);
    }
}
