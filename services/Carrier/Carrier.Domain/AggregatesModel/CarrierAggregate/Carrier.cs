namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public class Carrier
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;           // Company Name abbreviated
    public string? LocalName { get; set; }
    public string? ClientAdsCode { get; set; }
    public string? Okpo { get; set; }
    public string? Bin { get; set; }
    public string? Ogrn { get; set; }
    public string? Tin { get; set; }                           // TIN / taxpayer ID / VAT no
    public string? Rrc { get; set; }
    public string? VatNumber { get; set; }

    public Guid? CarrierTypeId { get; set; }                   // From settings Carrier types
    public Guid? TransportTypeId { get; set; }                 // Air, Sea, Road, Rail
    public string? CarrierDirection { get; set; }              // e.g. "AZE - KZ"
    public DateTime? DateOfCreation { get; set; }

    // Legal address
    public Guid? LegalCountryId { get; set; }
    public Guid? LegalStateId { get; set; }
    public Guid? LegalCityId { get; set; }
    public string? LegalZipCode { get; set; }
    public string? LegalPhones { get; set; }                   // Semicolon-separated for multiple
    public string? LegalFax { get; set; }
    public string? LegalEmails { get; set; }

    // Postal address
    public Guid? PostalCountryId { get; set; }
    public Guid? PostalStateId { get; set; }
    public Guid? PostalCityId { get; set; }
    public string? PostalZipCode { get; set; }
    public string? PostalPhones { get; set; }
    public string? PostalFax { get; set; }
    public string? PostalEmails { get; set; }

    // Payment
    public decimal? CreditLimit { get; set; }
    public int? PaymentDelay { get; set; }
    public Guid? DeferredPaymentConditionId { get; set; }

    public string? Comment { get; set; }                       // Notes / Additional field
    public bool IsDeactive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<CarrierContactPerson> ContactPersons { get; set; } = new List<CarrierContactPerson>();
    public ICollection<CarrierBankAccount> BankAccounts { get; set; } = new List<CarrierBankAccount>();
    public ICollection<CarrierManager> Managers { get; set; } = new List<CarrierManager>();
    public ICollection<CarrierDirection> Directions { get; set; } = new List<CarrierDirection>();
    public ICollection<CarrierDocument> Documents { get; set; } = new List<CarrierDocument>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
