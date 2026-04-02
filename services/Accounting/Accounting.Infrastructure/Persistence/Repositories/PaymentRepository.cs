using Accounting.Domain.AggregatesModel.PaymentAggregate;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence.Repositories;

public class PaymentRepository(AccountingDbContext context) : IPaymentRepository
{
    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Payment>> GetAllAsync(PaymentDirection? direction, CancellationToken cancellationToken = default)
    {
        var q = context.Payments.AsNoTracking();
        if (direction.HasValue)
            q = q.Where(p => p.Direction == direction.Value);
        return await q.OrderByDescending(p => p.CreatedAtUtc).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetByOperationInvoiceIdAsync(Guid operationInvoiceId,
        CancellationToken cancellationToken = default)
        => await context.Payments.AsNoTracking()
            .Where(p => p.OperationInvoiceId == operationInvoiceId)
            .OrderByDescending(p => p.CreatedAtUtc)
            .ToListAsync(cancellationToken);

    public async Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        context.Payments.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
