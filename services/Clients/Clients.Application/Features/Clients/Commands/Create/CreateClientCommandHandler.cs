using Clients.Application.DTOs.Client;
using Clients.Application.Features.Clients;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.ClientAggregate;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.Create;

public sealed class CreateClientCommandHandler(
    IClientRepository repository,
    IActionLogClient actionLogClient,
    ISettingsReferenceDataClient settingsReferenceData) : IRequestHandler<CreateClientCommand, ClientDto>
{
    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var g = dto.General;
        var legal = dto.Legal;
        var postal = dto.Postal;
        var pay = dto.Payment;

        var abbrev = string.IsNullOrWhiteSpace(g.NameAbbreviated)
            ? (g.CompanyName.Length > 200 ? g.CompanyName[..200] : g.CompanyName)
            : g.NameAbbreviated.Trim();

        var entity = new Client
        {
            Id = Guid.NewGuid(),
            Code = GenerateCode(),
            IsCustomer = g.IsCustomer,
            ShipperClientNotRequired = g.ShipperClientNotRequired,
            CompanyName = g.CompanyName,
            NameAbbreviated = abbrev,
            ManagerId = g.ManagerId,
            SalesmanId = g.SalesmanId,
            ClientSourceId = g.ClientSourceId,
            ClientStatusId = g.ClientStatusId,
            ClientTypeId = g.ClientTypeId,
            ActivityAreaId = g.ActivityAreaId,
            ActivityAreaName = g.ActivityAreaName,
            AddressLine1 = g.AddressLine1,
            VatNumber = g.VatNumber,
            Inn = g.Inn,
            Tin = g.Tin,
            Title = g.Title,
            Okpo = g.Okpo,
            Kpp = g.Kpp,
            Ogrn = g.Ogrn,
            Bin = g.Bin,
            ClientAisCode = g.ClientAisCode,
            PrimaryPhone = g.PrimaryPhone,
            GeneralFax = g.GeneralFax,
            LegalCountryId = legal.CountryId,
            LegalStateId = legal.StateId,
            LegalCityId = legal.CityId,
            LegalZipCode = legal.ZipCode,
            LegalStreet = legal.Street,
            LegalEmail = legal.Email,
            LegalFax = legal.Fax,
            PostalCountryId = postal.CountryId,
            PostalStateId = postal.StateId,
            PostalCityId = postal.CityId,
            PostalCityName = postal.CityName,
            PostalZipCode = postal.ZipCode,
            PostalStreet = postal.Street,
            PostalEmail = postal.Email,
            PostalPhone = postal.Phone,
            PostalMobile = postal.Mobile,
            PostalFax = postal.Fax,
            CreditLimit = pay.CreditLimit,
            DeferredPaymentConditionId = pay.DeferredPaymentConditionId,
            PaymentDelay = pay.PaymentDelay,
            EmailToSendDocuments = pay.EmailToSendDocuments,
            Comment = g.Comment,
            IsDeactive = dto.IsDeactive,
            CreatedAt = DateTime.UtcNow
        };

        foreach (var cp in dto.ContactPersons)
        {
            entity.ContactPersons.Add(new ClientContactPerson
            {
                Id = Guid.NewGuid(),
                ClientId = entity.Id,
                EnglishName = cp.EnglishName,
                Phone = cp.Phone,
                Email = cp.Email,
                Mobile = cp.Mobile,
                Fax = cp.Fax,
                WorkerPostId = cp.WorkerPostId
            });
        }

        foreach (var ba in dto.BankAccounts)
        {
            entity.BankAccounts.Add(new ClientBankAccount
            {
                Id = Guid.NewGuid(),
                ClientId = entity.Id,
                BankId = ba.BankId,
                CurrencyId = ba.CurrencyId,
                AccountNumberIban = ba.AccountNumberIban,
                TransitAmount = ba.TransitAmount,
                CorrespondentBankId = ba.CorrespondentBankId,
                CorrespondentAccount = ba.CorrespondentAccount
            });
        }

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var mapped = ClientMapper.MapToDto(created!);
        var result = await ClientResponseEnricher.EnrichFromSettingsAsync(mapped, settingsReferenceData, cancellationToken);
        await actionLogClient.LogAsync("Client created", $"client: {entity.CompanyName} • code: {entity.Code} • id: {entity.Id}", entity.ManagerId, null, cancellationToken);
        return result;
    }

    private static string GenerateCode() => "CL-" + Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();
}
