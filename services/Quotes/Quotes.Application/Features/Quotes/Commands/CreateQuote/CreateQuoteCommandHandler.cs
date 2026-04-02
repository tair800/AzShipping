using MediatR;
using Quotes.Application.DTOs.Quote;
using Quotes.Application.Features.Quotes.Queries.GetQuoteById;
using Quotes.Application.Services;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Application.Features.Quotes.Commands.CreateQuote;

public sealed class CreateQuoteCommandHandler(
    IQuoteRepository repository,
    IQuoteTypeRepository typeRepository,
    IActionLogClient actionLogClient) : IRequestHandler<CreateQuoteCommand, QuoteDto>
{
    public async Task<QuoteDto> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var quoteType = await typeRepository.GetByIdAsync(d.QuoteTypeId, cancellationToken)
            ?? throw new InvalidOperationException("QuoteType not found.");
        var quoteNumber = !string.IsNullOrWhiteSpace(d.QuoteNumber)
            ? d.QuoteNumber.Trim()
            : quoteType.QuoteNumberPrefix + (await repository.GetNextSequenceForPrefixAsync(quoteType.QuoteNumberPrefix, cancellationToken));
        var now = DateTime.UtcNow;
        var entity = MapToEntity(Guid.NewGuid(), quoteNumber, now, null, d);
        await repository.AddAsync(entity, cancellationToken);
        var result = await new GetQuoteByIdQueryHandler(repository, typeRepository).Handle(new GetQuoteByIdQuery(entity.Id), cancellationToken);
        await actionLogClient.LogAsync("Quote created", $"quote: {quoteNumber} • id: {entity.Id}", entity.ManagerId, entity.ManagerName, cancellationToken);
        return result!;
    }

    private static QuoteEntity MapToEntity(Guid id, string quoteNumber, DateTime createdAt, DateTime? updatedAt, CreateOrUpdateQuoteDto d)
    {
        return new QuoteEntity
        {
            Id = id,
            CreationDate = createdAt,
            QuoteNumber = quoteNumber,
            QuoteTypeId = d.QuoteTypeId,
            CompanyId = d.CompanyId,
            CompanyName = d.CompanyName,
            ManagerId = d.ManagerId,
            ManagerName = d.ManagerName,
            LogisticianId = d.LogisticianId,
            LogisticianName = d.LogisticianName,
            HandlerId = d.HandlerId,
            HandlerName = d.HandlerName,
            AccountManagerId = d.AccountManagerId,
            AccountManagerName = d.AccountManagerName,
            OpenedById = d.OpenedById,
            OpenedByName = d.OpenedByName,
            ManagerUserId = d.ManagerUserId,
            HandlerUserId = d.HandlerUserId,
            AccountManagerUserId = d.AccountManagerUserId,
            OpenedByUserId = d.OpenedByUserId,
            DepartmentId = d.DepartmentId,
            DepartmentName = d.DepartmentName,
            QuoteStatus = d.QuoteStatus,
            ShipperId = d.ShipperId,
            ShipperName = d.ShipperName,
            ConsigneeId = d.ConsigneeId,
            ConsigneeName = d.ConsigneeName,
            MyCustomerTypeId = d.MyCustomerTypeId,
            MyCustomerTypeName = d.MyCustomerTypeName,
            RateType = d.RateType,
            StartDate = d.StartDate,
            Etd = d.Etd,
            Eta = d.Eta,
            IncotermId = d.IncotermId,
            IncotermName = d.IncotermName,
            ExpirationDays = d.ExpirationDays,
            PurchaseFreeDays = d.PurchaseFreeDays,
            SaleFreeDays = d.SaleFreeDays,
            MoveTypeId = d.MoveTypeId,
            MoveTypeName = d.MoveTypeName,
            ExpirationDate = d.ExpirationDate,
            CloseAutomaticallyDeclined = d.CloseAutomaticallyDeclined,
            CloseAutomaticallyDeclinedDays = d.CloseAutomaticallyDeclinedDays,
            AutomaticallyCloseDate = d.AutomaticallyCloseDate,
            IncludeInsurance = d.IncludeInsurance ?? false,
            InsuranceValue = d.InsuranceValue,
            IsStackable = d.IsStackable ?? false,
            IncludeImportDutyCharges = d.IncludeImportDutyCharges ?? false,
            TransitTime = d.TransitTime,
            IsFreighter = d.IsFreighter ?? false,
            DepartureFrequency = d.DepartureFrequency,
            ValueOfGoods = d.ValueOfGoods,
            PriceStandard = d.PriceStandard,
            RmbVwt = d.RmbVwt,
            CurrencyId = d.CurrencyId,
            CurrencyCode = d.CurrencyCode,
            PriceWithVat = d.PriceWithVat,
            MinVat = d.MinVat,
            VatRate = d.VatRate,
            VatNote = d.VatNote,
            IncludePickup = d.IncludePickup ?? false,
            PickupAddressId = d.PickupAddressId,
            PickupCountryId = d.PickupCountryId,
            PickupCountryName = d.PickupCountryName,
            PickupStateId = d.PickupStateId,
            PickupStateName = d.PickupStateName,
            PickupCityId = d.PickupCityId,
            PickupCityName = d.PickupCityName,
            PickupZipCode = d.PickupZipCode,
            GatewayTerminalId = d.GatewayTerminalId,
            GatewayName = d.GatewayName,
            ViaPortTerminalId = d.ViaPortTerminalId,
            ViaPortName = d.ViaPortName,
            DestinationTerminalId = d.DestinationTerminalId,
            DestinationName = d.DestinationName,
            ViaPort2TerminalId = d.ViaPort2TerminalId,
            ViaPort2Name = d.ViaPort2Name,
            CarrierId = d.CarrierId,
            CarrierName = d.CarrierName,
            MyPortTerminalId = d.MyPortTerminalId,
            MyPortName = d.MyPortName,
            MyPort2TerminalId = d.MyPort2TerminalId,
            MyPort2Name = d.MyPort2Name,
            PortOfDeliveryName = d.PortOfDeliveryName,
            VasId = d.VasId,
            IncludeVas = d.IncludeVas ?? false,
            VasServiceName = d.VasServiceName,
            ExecutionPlace = d.ExecutionPlace,
            VasQuantity = d.VasQuantity,
            VasUom = d.VasUom,
            VasCurrencyCode = d.VasCurrencyCode,
            VasTotal = d.VasTotal,
            VasNotes = d.VasNotes,
            IncludeDelivery = d.IncludeDelivery ?? false,
            DeliveryAddressId = d.DeliveryAddressId,
            DeliveryCountryId = d.DeliveryCountryId,
            DeliveryCountryName = d.DeliveryCountryName,
            DeliveryStateId = d.DeliveryStateId,
            DeliveryStateName = d.DeliveryStateName,
            DeliveryCityId = d.DeliveryCityId,
            DeliveryCityName = d.DeliveryCityName,
            DeliveryZipCode = d.DeliveryZipCode,
            GrossWeightKg = d.GrossWeightKg,
            VolumeCbm = d.VolumeCbm,
            ChargeableWeightKg = d.ChargeableWeightKg,
            NumberOfPackages = d.NumberOfPackages,
            DangerousGoods = d.DangerousGoods ?? false,
            DescriptionOfGoods = d.DescriptionOfGoods,
            Quantity1 = d.Quantity1,
            Quantity2 = d.Quantity2,
            Quantity3 = d.Quantity3,
            Quantity4 = d.Quantity4,
            PackageType1 = d.PackageType1,
            PackageType2 = d.PackageType2,
            PackageType3 = d.PackageType3,
            PackageType4 = d.PackageType4,
            Quantity5 = d.Quantity5,
            PackageType5 = d.PackageType5,
            ShipperRef2 = d.ShipperRef2,
            ConsigneeRef2 = d.ConsigneeRef2,
            AgentId = d.AgentId,
            AgentName = d.AgentName,
            NotesToBePrinted = d.NotesToBePrinted,
            Notes = d.Notes,
            SentToCustomerAt = d.SentToCustomerAt,
            IsCancelled = d.IsCancelled ?? false,
            CancelledAt = d.CancelledAt,
            IsActive = d.IsActive ?? true,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };
    }
}
