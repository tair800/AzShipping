using Accounting.Application.DTOs.Payment;
using MediatR;

namespace Accounting.Application.Features.Payments.Queries.GetById;

public record GetPaymentByIdQuery(Guid Id) : IRequest<PaymentDto?>;
