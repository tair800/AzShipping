namespace Carrier.Application.DTOs.Carrier;

public record UpdateCarrierDto
{
    public string Name { get; init; } = string.Empty;
    public string? LocalName { get; init; }
    public string? ClientAdsCode { get; init; }
    public string? Okpo { get; init; }
    public string? Bin { get; init; }
    public string? Ogrn { get; init; }
    public string? Tin { get; init; }
    public string? Rrc { get; init; }
    public string? VatNumber { get; init; }
    public Guid? CarrierTypeId { get; init; }
    public Guid? TransportTypeId { get; init; }
    public string? CarrierDirection { get; init; }
    public DateTime? DateOfCreation { get; init; }

    public Guid? LegalCountryId { get; init; }
    public Guid? LegalStateId { get; init; }
    public Guid? LegalCityId { get; init; }
    public string? LegalZipCode { get; init; }
    public string? LegalPhones { get; init; }
    public string? LegalFax { get; init; }
    public string? LegalEmails { get; init; }

    public Guid? PostalCountryId { get; init; }
    public Guid? PostalStateId { get; init; }
    public Guid? PostalCityId { get; init; }
    public string? PostalZipCode { get; init; }
    public string? PostalPhones { get; init; }
    public string? PostalFax { get; init; }
    public string? PostalEmails { get; init; }

    public decimal? CreditLimit { get; init; }
    public int? PaymentDelay { get; init; }
    public Guid? DeferredPaymentConditionId { get; init; }

    public string? Comment { get; init; }
    public bool IsDeactive { get; init; }

    public IReadOnlyList<UpdateCarrierContactPersonDto> ContactPersons { get; init; } = [];
    public IReadOnlyList<UpdateCarrierBankAccountDto> BankAccounts { get; init; } = [];
    public IReadOnlyList<Guid> ManagerIds { get; init; } = [];
}

public record UpdateCarrierContactPersonDto
{
    public Guid? Id { get; init; }
    public string? EnglishName { get; init; }
    public string? Position { get; init; }
    public string? Emails { get; init; }
    public string? Phones { get; init; }
    public string? Fax { get; init; }
}

public record UpdateCarrierBankAccountDto
{
    public Guid? Id { get; init; }
    public string? CurrencyCode { get; init; }
    public string? AccountNumber { get; init; }
    public Guid? BankId { get; init; }
    public string? TransitAccount { get; init; }
    public string? CorrespondentBank { get; init; }
    public string? CorrespondentAccount { get; init; }
}
