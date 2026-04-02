using Accounting.Application.DTOs.Payment;
using MediatR;

namespace Accounting.Application.Features.Payments.Commands.Create;

public record CreatePaymentCommand(CreatePaymentDto Dto) : IRequest<PaymentDto>;
