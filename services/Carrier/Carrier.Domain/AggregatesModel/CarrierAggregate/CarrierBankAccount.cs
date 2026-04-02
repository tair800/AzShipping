namespace Carrier.Domain.AggregatesModel.CarrierAggregate;

public class CarrierBankAccount
{
    public Guid Id { get; set; }
    public Guid CarrierId { get; set; }
    public Carrier Carrier { get; set; } = null!;
    public string? CurrencyCode { get; set; }          // From currencies (AZN, USD, etc.)
    public string? AccountNumber { get; set; }
    public Guid? BankId { get; set; }                  // From settings Bank
    public string? TransitAccount { get; set; }
    public string? CorrespondentBank { get; set; }     // Manual input
    public string? CorrespondentAccount { get; set; }
}
