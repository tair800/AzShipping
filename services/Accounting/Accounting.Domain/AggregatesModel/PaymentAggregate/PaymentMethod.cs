namespace Accounting.Domain.AggregatesModel.PaymentAggregate;

/// <summary>How the payment was made (matches UI payment type dropdown).</summary>
public enum PaymentMethod
{
    Cash = 0,
    NonCash = 1,
    BankTransfer = 2,
    Card = 3,
}
