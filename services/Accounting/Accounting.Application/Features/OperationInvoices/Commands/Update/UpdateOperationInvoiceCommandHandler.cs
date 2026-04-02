using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Application.Features.OperationInvoices;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Update;

public class UpdateOperationInvoiceCommandHandler(
    IOperationInvoiceRepository invoiceRepo,
    IPaymentRepository paymentRepo)
    : IRequestHandler<UpdateOperationInvoiceCommand, OperationInvoiceDto?>
{
    public async Task<OperationInvoiceDto?> Handle(UpdateOperationInvoiceCommand request, CancellationToken cancellationToken)
    {
        var existing = await invoiceRepo.GetByIdWithLinesAsync(request.Id, cancellationToken);
        if (existing == null)
            return null;

        var dto = request.Dto;
        if (dto.OperationId == Guid.Empty)
            throw new InvalidOperationException("OperationId is required.");

        var number = dto.InvoiceNumber?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(number))
            throw new InvalidOperationException("InvoiceNumber is required.");

        var cur = dto.CurrencyCode?.Trim() ?? string.Empty;
        if (cur.Length is < 3 or > 8)
            throw new InvalidOperationException("CurrencyCode must be between 3 and 8 characters (e.g. USD).");

        if (dto.Lines == null || dto.Lines.Count == 0)
            throw new InvalidOperationException("At least one invoice line is required.");

        var inv = new OperationInvoice
        {
            Id = request.Id,
            OperationId = dto.OperationId,
            CreatedAtUtc = existing.CreatedAtUtc,
            UpdatedAtUtc = DateTime.UtcNow
        };
        OperationInvoiceMapper.ApplyHeader(dto, inv, number, cur.ToUpperInvariant());

        var i = 0;
        foreach (var l in dto.Lines)
            inv.Lines.Add(OperationInvoiceMapper.ToLineEntity(l, i++));

        OperationInvoiceMapper.ApplySummary(dto, inv);
        OperationInvoiceMapper.AppendAdjustmentLines(inv, dto);

        OperationInvoiceCalculator.RecalculateHeader(inv);
        await invoiceRepo.UpdateAsync(inv, cancellationToken);

        var refreshed = await invoiceRepo.GetByIdWithLinesAsync(request.Id, cancellationToken);
        if (refreshed == null)
            return null;

        var payments = await paymentRepo.GetByOperationInvoiceIdAsync(request.Id, cancellationToken);
        var paid = OperationInvoicePaymentTotals.SumIncomingInInvoiceCurrency(payments, refreshed.CurrencyCode);
        return OperationInvoiceMapper.ToDto(refreshed, paid);
    }
}
