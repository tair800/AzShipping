using Accounting.Application.DTOs.Payment;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.Payments.Queries.GetAll;

public record GetAllPaymentsQuery(PaymentDirection? Direction = null) : IRequest<IReadOnlyList<PaymentDto>>;
