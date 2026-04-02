using Accounting.Application.DTOs.Payment;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.Payments.Queries.GetById;

public class GetPaymentByIdQueryHandler(IPaymentRepository paymentRepo)
    : IRequestHandler<GetPaymentByIdQuery, PaymentDto?>
{
    public async Task<PaymentDto?> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await paymentRepo.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : PaymentMapper.MapToDto(e);
    }
}
