using Accounting.Application.DTOs.Payment;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.Payments.Commands.Create;

public class CreatePaymentCommandHandler(IPaymentRepository paymentRepo)
    : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    public async Task<PaymentDto> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var currency = dto.CurrencyCode?.Trim() ?? string.Empty;
        if (currency.Length is < 3 or > 8)
            throw new InvalidOperationException("CurrencyCode must be between 3 and 8 characters (e.g. USD).");

        if (dto.PaidAmount <= 0)
            throw new InvalidOperationException("PaidAmount must be greater than zero.");

        if (!Enum.IsDefined(typeof(PaymentMethod), dto.PaymentMethod))
            throw new InvalidOperationException($"Invalid PaymentMethod value: {dto.PaymentMethod}.");

        if (!Enum.IsDefined(typeof(PaymentDirection), dto.Direction))
            throw new InvalidOperationException($"Invalid Direction value: {dto.Direction} (0=Incoming, 1=Outgoing).");

        var entity = new Payment
        {
            Id = Guid.NewGuid(),
            CreatedAtUtc = DateTime.UtcNow,
            Direction = (PaymentDirection)dto.Direction,
            CurrencyCode = currency.ToUpperInvariant(),
            PaidAmount = dto.PaidAmount,
            PaymentMethod = (PaymentMethod)dto.PaymentMethod,
            PaymentDate = dto.PaymentDate,
            ReceivedBy = string.IsNullOrWhiteSpace(dto.ReceivedBy) ? null : dto.ReceivedBy.Trim(),
            Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim(),
            OrderNo = string.IsNullOrWhiteSpace(dto.OrderNo) ? null : dto.OrderNo.Trim(),
            AccountLabel = string.IsNullOrWhiteSpace(dto.AccountLabel) ? null : dto.AccountLabel.Trim(),
            InvoiceReference = string.IsNullOrWhiteSpace(dto.InvoiceReference) ? null : dto.InvoiceReference.Trim(),
            OperationInvoiceId = dto.OperationInvoiceId,
            CounterpartyName = string.IsNullOrWhiteSpace(dto.CounterpartyName) ? null : dto.CounterpartyName.Trim(),
        };

        await paymentRepo.AddAsync(entity, cancellationToken);
        return PaymentMapper.MapToDto(entity);
    }
}
