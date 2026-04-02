namespace Accounting.Domain.AggregatesModel.PaymentAggregate;

public interface IPaymentRepository
{
    System.Threading.Tasks.Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<Payment>> GetAllAsync(PaymentDirection? direction, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<IReadOnlyList<Payment>> GetByOperationInvoiceIdAsync(Guid operationInvoiceId, CancellationToken cancellationToken = default);
    System.Threading.Tasks.Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default);
}
