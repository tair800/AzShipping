using Clients.Application.DTOs.Client;
using Clients.Application.Features.Clients;
using Clients.Application.Services;
using Clients.Domain.AggregatesModel.ClientAggregate;
using MediatR;

namespace Clients.Application.Features.Clients.Commands.Update;

public sealed class UpdateClientCommandHandler(
    IClientRepository repository,
    IActionLogClient actionLogClient,
    ISettingsReferenceDataClient settingsReferenceData) : IRequestHandler<UpdateClientCommand, ClientDto?>
{
    public async Task<ClientDto?> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        var g = dto.General;
        var legal = dto.Legal;
        var postal = dto.Postal;
        var pay = dto.Payment;

        var abbrev = string.IsNullOrWhiteSpace(g.NameAbbreviated)
            ? (g.CompanyName.Length > 200 ? g.CompanyName[..200] : g.CompanyName)
            : g.NameAbbreviated.Trim();

        entity.IsCustomer = g.IsCustomer;
        entity.ShipperClientNotRequired = g.ShipperClientNotRequired;
        entity.CompanyName = g.CompanyName;
        entity.NameAbbreviated = abbrev;
        entity.ManagerId = g.ManagerId;
        entity.SalesmanId = g.SalesmanId;
        entity.ClientSourceId = g.ClientSourceId;
        entity.ClientStatusId = g.ClientStatusId;
        entity.ClientTypeId = g.ClientTypeId;
        entity.ActivityAreaId = g.ActivityAreaId;
        entity.ActivityAreaName = g.ActivityAreaName;
        entity.AddressLine1 = g.AddressLine1;
        entity.VatNumber = g.VatNumber;
        entity.Inn = g.Inn;
        entity.Tin = g.Tin;
        entity.Title = g.Title;
        entity.Okpo = g.Okpo;
        entity.Kpp = g.Kpp;
        entity.Ogrn = g.Ogrn;
        entity.Bin = g.Bin;
        entity.ClientAisCode = g.ClientAisCode;
        entity.PrimaryPhone = g.PrimaryPhone;
        entity.GeneralFax = g.GeneralFax;
        entity.LegalCountryId = legal.CountryId;
        entity.LegalStateId = legal.StateId;
        entity.LegalCityId = legal.CityId;
        entity.LegalZipCode = legal.ZipCode;
        entity.LegalStreet = legal.Street;
        entity.LegalEmail = legal.Email;
        entity.LegalFax = legal.Fax;
        entity.PostalCountryId = postal.CountryId;
        entity.PostalStateId = postal.StateId;
        entity.PostalCityId = postal.CityId;
        entity.PostalCityName = postal.CityName;
        entity.PostalZipCode = postal.ZipCode;
        entity.PostalStreet = postal.Street;
        entity.PostalEmail = postal.Email;
        entity.PostalPhone = postal.Phone;
        entity.PostalMobile = postal.Mobile;
        entity.PostalFax = postal.Fax;
        entity.CreditLimit = pay.CreditLimit;
        entity.DeferredPaymentConditionId = pay.DeferredPaymentConditionId;
        entity.PaymentDelay = pay.PaymentDelay;
        entity.EmailToSendDocuments = pay.EmailToSendDocuments;
        entity.Comment = g.Comment;
        entity.IsDeactive = dto.IsDeactive;
        entity.UpdatedAt = DateTime.UtcNow;

        entity.ContactPersons.Clear();
        foreach (var cp in dto.ContactPersons)
        {
            entity.ContactPersons.Add(new ClientContactPerson
            {
                Id = cp.Id ?? Guid.NewGuid(),
                ClientId = entity.Id,
                EnglishName = cp.EnglishName,
                Phone = cp.Phone,
                Email = cp.Email,
                Mobile = cp.Mobile,
                Fax = cp.Fax,
                WorkerPostId = cp.WorkerPostId
            });
        }

        entity.BankAccounts.Clear();
        foreach (var ba in dto.BankAccounts)
        {
            entity.BankAccounts.Add(new ClientBankAccount
            {
                Id = ba.Id ?? Guid.NewGuid(),
                ClientId = entity.Id,
                BankId = ba.BankId,
                CurrencyId = ba.CurrencyId,
                AccountNumberIban = ba.AccountNumberIban,
                TransitAmount = ba.TransitAmount,
                CorrespondentBankId = ba.CorrespondentBankId,
                CorrespondentAccount = ba.CorrespondentAccount
            });
        }

        await repository.UpdateAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var mapped = ClientMapper.MapToDto(updated!);
        var result = await ClientResponseEnricher.EnrichFromSettingsAsync(mapped, settingsReferenceData, cancellationToken);
        await actionLogClient.LogAsync("Client updated", $"client: {entity.CompanyName} • code: {entity.Code} • id: {entity.Id}", entity.ManagerId, null, cancellationToken);
        return result;
    }
}
