using Accounting.Application.DTOs.Payment;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.Payments.Queries.GetAll;

public class GetAllPaymentsQueryHandler(IPaymentRepository paymentRepo)
    : IRequestHandler<GetAllPaymentsQuery, IReadOnlyList<PaymentDto>>
{
    public async Task<IReadOnlyList<PaymentDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var list = await paymentRepo.GetAllAsync(request.Direction, cancellationToken);
        return list.Select(PaymentMapper.MapToDto).ToList();
    }
}
