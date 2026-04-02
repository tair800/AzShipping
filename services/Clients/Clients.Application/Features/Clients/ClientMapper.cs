using Clients.Application.DTOs.Client;
using Clients.Domain.AggregatesModel.ClientAggregate;

namespace Clients.Application.Features.Clients;

public static class ClientMapper
{
    public static ClientDto MapToDto(Client e) => new()
    {
        Id = e.Id,
        Code = e.Code,
        IsDeactive = e.IsDeactive,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt,
        General = MapGeneral(e),
        Legal = MapLegal(e),
        Postal = MapPostal(e),
        Payment = MapPayment(e),
        ContactPersons = e.ContactPersons.Select(cp => new ClientContactPersonDto
        {
            Id = cp.Id,
            EnglishName = cp.EnglishName,
            Phone = cp.Phone,
            Email = cp.Email,
            Mobile = cp.Mobile,
            Fax = cp.Fax,
            WorkerPostId = cp.WorkerPostId
        }).ToList(),
        BankAccounts = e.BankAccounts.Select(ba => new ClientBankAccountDto
        {
            Id = ba.Id,
            BankId = ba.BankId,
            CurrencyId = ba.CurrencyId,
            AccountNumberIban = ba.AccountNumberIban,
            TransitAmount = ba.TransitAmount,
            CorrespondentBankId = ba.CorrespondentBankId,
            CorrespondentAccount = ba.CorrespondentAccount
        }).ToList()
    };

    private static ClientGeneralInformationDto MapGeneral(Client e) => new()
    {
        IsCustomer = e.IsCustomer,
        ShipperClientNotRequired = e.ShipperClientNotRequired,
        CompanyName = e.CompanyName,
        NameAbbreviated = e.NameAbbreviated,
        ManagerId = e.ManagerId,
        SalesmanId = e.SalesmanId,
        ClientSourceId = e.ClientSourceId,
        ClientStatusId = e.ClientStatusId,
        ClientTypeId = e.ClientTypeId,
        ActivityAreaId = e.ActivityAreaId,
        ActivityAreaName = e.ActivityAreaName,
        AddressLine1 = e.AddressLine1,
        VatNumber = e.VatNumber,
        Inn = e.Inn,
        Tin = e.Tin,
        Title = e.Title,
        Okpo = e.Okpo,
        Kpp = e.Kpp,
        Ogrn = e.Ogrn,
        Bin = e.Bin,
        ClientAisCode = e.ClientAisCode,
        PrimaryPhone = e.PrimaryPhone,
        GeneralFax = e.GeneralFax,
        Comment = e.Comment
    };

    private static ClientLegalAddressDto MapLegal(Client e) => new()
    {
        CountryId = e.LegalCountryId,
        StateId = e.LegalStateId,
        CityId = e.LegalCityId,
        ZipCode = e.LegalZipCode,
        Street = e.LegalStreet,
        Email = e.LegalEmail,
        Fax = e.LegalFax
    };

    private static ClientPostalInformationDto MapPostal(Client e) => new()
    {
        CountryId = e.PostalCountryId,
        StateId = e.PostalStateId,
        CityId = e.PostalCityId,
        CityName = e.PostalCityName,
        ZipCode = e.PostalZipCode,
        Street = e.PostalStreet,
        Email = e.PostalEmail,
        Phone = e.PostalPhone,
        Mobile = e.PostalMobile,
        Fax = e.PostalFax
    };

    private static ClientPaymentInformationDto MapPayment(Client e) => new()
    {
        CreditLimit = e.CreditLimit,
        DeferredPaymentConditionId = e.DeferredPaymentConditionId,
        PaymentDelay = e.PaymentDelay,
        EmailToSendDocuments = e.EmailToSendDocuments
    };
}
