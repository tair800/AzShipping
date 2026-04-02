using Accounting.Domain;
using MediatR;
using Request.Application.DTOs.PriceProposal;
using Request.Application.Features.PriceProposals;
using Request.Application.Services;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.PriceProposals.Commands.Update;

public sealed class UpdatePriceProposalCommandHandler(
    IPriceProposalRepository repository,
    IVatRateLookupService vatRateLookup) : IRequestHandler<UpdatePriceProposalCommand, PriceProposalDto?>
{
    public async Task<PriceProposalDto?> Handle(UpdatePriceProposalCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        var d = request.Dto;

        if (d.ClientPrice.HasValue && d.ClientVatRateId.HasValue)
        {
            var vatPercent = await vatRateLookup.GetVatPercentAsync(d.ClientVatRateId, cancellationToken);
            if (vatPercent.HasValue)
                entity.ClientPriceWithVat = VatCalculation.GrossFromNet(d.ClientPrice.Value, vatPercent.Value);
        }
        else if (d.ClientPriceWithVat != null) entity.ClientPriceWithVat = d.ClientPriceWithVat;

        if (d.CarrierRate.HasValue && d.CarrierVatRateId.HasValue)
        {
            var vatPercent = await vatRateLookup.GetVatPercentAsync(d.CarrierVatRateId, cancellationToken);
            if (vatPercent.HasValue)
                entity.CarrierRateWithVat = VatCalculation.GrossFromNet(d.CarrierRate.Value, vatPercent.Value);
        }
        else if (d.CarrierRateWithVat != null) entity.CarrierRateWithVat = d.CarrierRateWithVat;

        if (d.Type != null) entity.Type = d.Type;
        if (d.TemplateName != null) entity.TemplateName = d.TemplateName;
        if (d.CarrierId != null) entity.CarrierId = d.CarrierId;
        if (d.CarrierName != null) entity.CarrierName = d.CarrierName;
        if (d.TypeOfService != null) entity.TypeOfService = d.TypeOfService;
        if (d.Name != null) entity.Name = d.Name;
        if (d.ClientPrice != null) entity.ClientPrice = d.ClientPrice;
        // ClientPriceWithVat set above when ClientPrice + ClientVatRateId provided
        if (d.ClientVatRateId != null) entity.ClientVatRateId = d.ClientVatRateId;
        if (d.ClientVatRateCode != null) entity.ClientVatRateCode = d.ClientVatRateCode;
        if (d.ClientCurrencyId != null) entity.ClientCurrencyId = d.ClientCurrencyId;
        if (d.ClientCurrencyCode != null) entity.ClientCurrencyCode = d.ClientCurrencyCode;
        if (d.SeparateLineInInvoice != null) entity.SeparateLineInInvoice = d.SeparateLineInInvoice.Value;
        if (d.CarrierRate != null) entity.CarrierRate = d.CarrierRate;
        // CarrierRateWithVat set above when CarrierRate + CarrierVatRateId provided
        if (d.CarrierVatRateId != null) entity.CarrierVatRateId = d.CarrierVatRateId;
        if (d.CarrierVatRateCode != null) entity.CarrierVatRateCode = d.CarrierVatRateCode;
        if (d.CarrierCurrencyId != null) entity.CarrierCurrencyId = d.CarrierCurrencyId;
        if (d.CarrierCurrencyCode != null) entity.CarrierCurrencyCode = d.CarrierCurrencyCode;
        if (d.Expense != null) entity.Expense = d.Expense;
        if (d.Profit != null) entity.Profit = d.Profit;
        if (d.Route != null) entity.Route = d.Route;
        if (d.Comments != null) entity.Comments = d.Comments;
        if (d.CargoItems != null)
        {
            entity.CargoItems.Clear();
            foreach (var c in d.CargoItems)
            {
                entity.CargoItems.Add(new PriceProposalCargo
                {
                    Id = Guid.NewGuid(),
                    PriceProposalId = entity.Id,
                    Description = c.Description,
                    Quantity = c.Quantity,
                    PackageType = c.PackageType,
                    IncludeInsurance = c.IncludeInsurance,
                    DescriptionOfGoods = c.DescriptionOfGoods
                });
            }
        }
        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(request.Id, cancellationToken);
        return loaded == null ? null : PriceProposalMapper.MapToDto(loaded);
    }
}
