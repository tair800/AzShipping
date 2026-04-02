namespace Accounting.Domain.AggregatesModel.PaymentAggregate;

/// <summary>
/// <see cref="Incoming"/> — client paid you (AR). <see cref="Outgoing"/> — you paid carrier/vendor (AP, “payments made”).
/// </summary>
public enum PaymentDirection
{
    Incoming = 0,
    Outgoing = 1,
}
