namespace Clients.Domain.AggregatesModel.ClientAggregate;

/// <summary>
/// Persisted as one row; API maps this to section DTOs (general, legal, postal, payment, contacts, bank accounts).
/// </summary>
public class Client
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;

    // General (company, refs, identifiers, comment) — see ClientGeneralInformationDto
    public bool IsCustomer { get; set; } = true;
    public bool ShipperClientNotRequired { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string NameAbbreviated { get; set; } = string.Empty;
    public Guid? ManagerId { get; set; }
    public Guid? SalesmanId { get; set; }
    public Guid? ClientSourceId { get; set; }
    public Guid? ClientStatusId { get; set; }
    public Guid? ActivityAreaId { get; set; }
    /// <summary>Free-text activity / industry area until a master-data catalog is available.</summary>
    public string? ActivityAreaName { get; set; }
    /// <summary>Lookup in Settings (e.g. client segment) for client type, distinct from stage.</summary>
    public Guid? ClientTypeId { get; set; }
    public string? VatNumber { get; set; }
    public string? Inn { get; set; }
    /// <summary>Secondary tax identifier (e.g. TIN) separate from INN / VAT.</summary>
    public string? Tin { get; set; }
    public string? Title { get; set; }
    public string? Okpo { get; set; }
    public string? Kpp { get; set; }
    public string? Ogrn { get; set; }
    public string? Bin { get; set; }
    public string? ClientAisCode { get; set; }
    /// <summary>Primary phone on the general tab (main contact line).</summary>
    public string? PrimaryPhone { get; set; }
    /// <summary>Fax on general information (separate from legal/postal fax).</summary>
    public string? GeneralFax { get; set; }
    /// <summary>First address line on general tab (may differ from legal street).</summary>
    public string? AddressLine1 { get; set; }

    // Legal address — see ClientLegalAddressDto
    public Guid? LegalCountryId { get; set; }
    public Guid? LegalStateId { get; set; }
    public Guid? LegalCityId { get; set; }
    public string? LegalZipCode { get; set; }
    public string? LegalStreet { get; set; }
    public string? LegalEmail { get; set; }
    public string? LegalFax { get; set; }

    // Postal / correspondence — see ClientPostalInformationDto
    public Guid? PostalCountryId { get; set; }
    public Guid? PostalStateId { get; set; }
    public Guid? PostalCityId { get; set; }
    /// <summary>Postal city as entered on the form (may differ from <see cref="PostalCityId"/> lookup).</summary>
    public string? PostalCityName { get; set; }
    public string? PostalZipCode { get; set; }
    public string? PostalStreet { get; set; }
    public string? PostalEmail { get; set; }
    public string? PostalPhone { get; set; }
    public string? PostalMobile { get; set; }
    public string? PostalFax { get; set; }

    // Payment — see ClientPaymentInformationDto
    public decimal? CreditLimit { get; set; }
    public Guid? DeferredPaymentConditionId { get; set; }
    public int? PaymentDelay { get; set; }
    public string? EmailToSendDocuments { get; set; }

    public string? Comment { get; set; }
    public bool IsDeactive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ClientContactPerson> ContactPersons { get; set; } = new List<ClientContactPerson>();
    public ICollection<ClientBankAccount> BankAccounts { get; set; } = new List<ClientBankAccount>();
}
