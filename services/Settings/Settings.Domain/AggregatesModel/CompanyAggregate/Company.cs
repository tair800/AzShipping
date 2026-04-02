using Settings.Domain.AggregatesModel.CityAggregate;
using Settings.Domain.AggregatesModel.CountryAggregate;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;
using Settings.Domain.AggregatesModel.WorkerPostAggregate;

namespace Settings.Domain.AggregatesModel.CompanyAggregate;

public class Company
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;           // Name (abbreviated)
    public string? NameFull { get; set; }                       // Name (full)
    public string? DirectorsFullName { get; set; }
    public string? InTheNameOfWhom { get; set; }
    public Guid? WorkerPostId { get; set; }                     // Director/post from WorkerPosts
    public string? Post { get; set; }                           // Legacy/manual override
    public string? VatRate { get; set; }                        // From VAT page if exists; null for now
    public Guid? PricingTypeId { get; set; }                    // From PricingTypes
    public string? PricingType { get; set; }                    // Legacy/manual override
    public string? CompanyPrefix { get; set; }
    public string? CompanyCodeType { get; set; }                // e.g. INN
    public string? CompanyCode { get; set; }
    public string? VatCode { get; set; }
    public string? Rrc { get; set; }
    public string? CorrespondentAccount { get; set; }
    public string? Okpo { get; set; }
    public string? Ogrn { get; set; }

    // Contact information
    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
    public Guid? StateId { get; set; }
    public State? State { get; set; }
    public Guid? CityId { get; set; }
    public City? City { get; set; }
    public string? Address { get; set; }
    public string? PostCode { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public bool IsMainCompany { get; set; }

    // Correspondent information
    public Guid? CorrespondentCountryId { get; set; }
    public Country? CorrespondentCountry { get; set; }
    public Guid? CorrespondentStateId { get; set; }
    public State? CorrespondentState { get; set; }
    public Guid? CorrespondentCityId { get; set; }
    public City? CorrespondentCity { get; set; }
    public string? CorrespondentAddress { get; set; }
    public string? CorrespondentPostCode { get; set; }
    public string? CorrespondentTelephone { get; set; }
    public string? CorrespondentFax { get; set; }
    public string? CorrespondentEmail { get; set; }
    public string? CorrespondentWebsite { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public WorkerPost? WorkerPost { get; set; }
    public PricingType? PricingTypeEntity { get; set; }

    public ICollection<CompanyBankAccount> BankAccounts { get; set; } = new List<CompanyBankAccount>();
    public ICollection<CompanySignature> Signatures { get; set; } = new List<CompanySignature>();
}
