namespace Settings.Domain.AggregatesModel.CompanyAggregate;

public class CompanyBankAccount
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public Guid? BankId { get; set; }
    public string? CurrencyCode { get; set; }
    public string? AccountNumberIban { get; set; }
    public string? BankCode { get; set; }
    public string? Swift { get; set; }
    public string? TransitAmount { get; set; }
    public Guid? CorrespondentBankId { get; set; }
    public string? CorrespondentAccount { get; set; }
}
