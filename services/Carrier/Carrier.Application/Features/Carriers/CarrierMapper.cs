using Carrier.Application.DTOs.Carrier;
using CarrierDomain = Carrier.Domain.AggregatesModel.CarrierAggregate;

namespace Carrier.Application.Features.Carriers;

public static class CarrierMapper
{
    public static CarrierDto MapToDto(CarrierDomain.Carrier e) => new()
    {
        Id = e.Id,
        Name = e.Name,
        LocalName = e.LocalName,
        ClientAdsCode = e.ClientAdsCode,
        Okpo = e.Okpo,
        Bin = e.Bin,
        Ogrn = e.Ogrn,
        Tin = e.Tin,
        Rrc = e.Rrc,
        VatNumber = e.VatNumber,
        CarrierTypeId = e.CarrierTypeId,
        TransportTypeId = e.TransportTypeId,
        CarrierDirection = e.CarrierDirection,
        DateOfCreation = e.DateOfCreation,
        LegalCountryId = e.LegalCountryId,
        LegalStateId = e.LegalStateId,
        LegalCityId = e.LegalCityId,
        LegalZipCode = e.LegalZipCode,
        LegalPhones = e.LegalPhones,
        LegalFax = e.LegalFax,
        LegalEmails = e.LegalEmails,
        PostalCountryId = e.PostalCountryId,
        PostalStateId = e.PostalStateId,
        PostalCityId = e.PostalCityId,
        PostalZipCode = e.PostalZipCode,
        PostalPhones = e.PostalPhones,
        PostalFax = e.PostalFax,
        PostalEmails = e.PostalEmails,
        CreditLimit = e.CreditLimit,
        PaymentDelay = e.PaymentDelay,
        DeferredPaymentConditionId = e.DeferredPaymentConditionId,
        Comment = e.Comment,
        IsDeactive = e.IsDeactive,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt,
        ContactPersons = e.ContactPersons.Select(cp => new CarrierContactPersonDto
        {
            Id = cp.Id,
            EnglishName = cp.EnglishName,
            Position = cp.Position,
            Emails = cp.Emails,
            Phones = cp.Phones,
            Fax = cp.Fax
        }).ToList(),
        BankAccounts = e.BankAccounts.Select(ba => new CarrierBankAccountDto
        {
            Id = ba.Id,
            CurrencyCode = ba.CurrencyCode,
            AccountNumber = ba.AccountNumber,
            BankId = ba.BankId,
            TransitAccount = ba.TransitAccount,
            CorrespondentBank = ba.CorrespondentBank,
            CorrespondentAccount = ba.CorrespondentAccount
        }).ToList(),
        ManagerIds = e.Managers.Select(m => m.UserId).ToList()
    };
}
