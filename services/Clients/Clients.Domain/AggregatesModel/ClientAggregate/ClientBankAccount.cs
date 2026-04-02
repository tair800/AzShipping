namespace Clients.Domain.AggregatesModel.ClientAggregate;

public class ClientBankAccount
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public Guid? BankId { get; set; }
    public Guid? CurrencyId { get; set; }
    public string? AccountNumberIban { get; set; }
    public string? TransitAmount { get; set; }
    public Guid? CorrespondentBankId { get; set; }
    public string? CorrespondentAccount { get; set; }
}
