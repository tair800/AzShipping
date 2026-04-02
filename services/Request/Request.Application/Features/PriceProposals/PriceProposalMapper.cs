using Request.Application.DTOs.PriceProposal;
using Request.Domain.AggregatesModel.PriceProposalAggregate;

namespace Request.Application.Features.PriceProposals;

public static class PriceProposalMapper
{
    public static PriceProposalDto MapToDto(PriceProposal entity)
    {
        var cargo = (entity.CargoItems ?? []).Select(c => new PriceProposalCargoDto(
            c.Id, c.PriceProposalId, c.Description, c.Quantity, c.PackageType,
            c.IncludeInsurance, c.DescriptionOfGoods)).ToList();
        return new PriceProposalDto(
            entity.Id, entity.RequestId, entity.Type ?? "Calculation",
            entity.TemplateName, entity.CarrierId, entity.CarrierName, entity.TypeOfService,
            entity.Name, entity.ClientPrice, entity.ClientPriceWithVat,
            entity.ClientVatRateId, entity.ClientVatRateCode, entity.ClientCurrencyId, entity.ClientCurrencyCode,
            entity.SeparateLineInInvoice, entity.CarrierRate, entity.CarrierRateWithVat,
            entity.CarrierVatRateId, entity.CarrierVatRateCode, entity.CarrierCurrencyId, entity.CarrierCurrencyCode,
            entity.Expense, entity.Profit, entity.Route, entity.Comments,
            entity.CreatedAt, entity.UserId, entity.UserName, cargo);
    }
}
