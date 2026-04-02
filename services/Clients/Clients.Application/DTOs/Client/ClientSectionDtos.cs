namespace Clients.Application.DTOs.Client;

/// <summary>General company data, identifiers, and notes (form tab: General information).</summary>
public record ClientGeneralInformationDto
{
    public bool IsCustomer { get; init; } = true;
    public bool ShipperClientNotRequired { get; init; }
    public string CompanyName { get; init; } = string.Empty;
    public string NameAbbreviated { get; init; } = string.Empty;
    public Guid? ManagerId { get; init; }
    public Guid? SalesmanId { get; init; }
    public Guid? ClientSourceId { get; init; }
    public Guid? ClientStatusId { get; init; }
    public Guid? ClientTypeId { get; init; }
    public Guid? ActivityAreaId { get; init; }
    public string? ActivityAreaName { get; init; }
    public string? AddressLine1 { get; init; }
    public string? VatNumber { get; init; }
    public string? Inn { get; init; }
    public string? Tin { get; init; }
    public string? Title { get; init; }
    public string? Okpo { get; init; }
    public string? Kpp { get; init; }
    public string? Ogrn { get; init; }
    public string? Bin { get; init; }
    public string? ClientAisCode { get; init; }
    public string? PrimaryPhone { get; init; }
    public string? GeneralFax { get; init; }
    public string? Comment { get; init; }
}

/// <summary>Legal / registration address (distinct from postal delivery address).</summary>
public record ClientLegalAddressDto
{
    public Guid? CountryId { get; init; }
    public Guid? StateId { get; init; }
    public Guid? CityId { get; init; }
    public string? ZipCode { get; init; }
    public string? Street { get; init; }
    public string? Email { get; init; }
    public string? Fax { get; init; }
}

/// <summary>Postal / correspondence address and contact lines for that location.</summary>
public record ClientPostalInformationDto
{
    public Guid? CountryId { get; init; }
    public Guid? StateId { get; init; }
    public Guid? CityId { get; init; }
    /// <summary>City name as captured on the postal tab (free text or matched to <see cref="CityId"/>).</summary>
    public string? CityName { get; init; }
    public string? ZipCode { get; init; }
    public string? Street { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? Mobile { get; init; }
    public string? Fax { get; init; }
}

/// <summary>Payment terms and billing delivery (form tab: Payment).</summary>
public record ClientPaymentInformationDto
{
    public decimal? CreditLimit { get; init; }
    public Guid? DeferredPaymentConditionId { get; init; }
    public int? PaymentDelay { get; init; }
    public string? EmailToSendDocuments { get; init; }

    /// <summary>Populated from Settings when returning the client (not sent on create/update).</summary>
    public ClientDeferredPaymentConditionSnapshotDto? DeferredPaymentCondition { get; init; }
}
