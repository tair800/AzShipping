using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Application.Features.OperationInvoices;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Create;

public class CreateOperationInvoiceCommandHandler(IOperationInvoiceRepository invoiceRepo)
    : IRequestHandler<CreateOperationInvoiceCommand, OperationInvoiceDto>
{
    public async Task<OperationInvoiceDto> Handle(CreateOperationInvoiceCommand request, CancellationToken cancellationToken)
    {
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

        var now = DateTime.UtcNow;
        var inv = new OperationInvoice
        {
            Id = Guid.NewGuid(),
            OperationId = dto.OperationId,
            CreatedAtUtc = now
        };
        OperationInvoiceMapper.ApplyHeader(dto, inv, number, cur.ToUpperInvariant());

        var i = 0;
        foreach (var l in dto.Lines)
            inv.Lines.Add(OperationInvoiceMapper.ToLineEntity(l, i++));

        OperationInvoiceMapper.ApplySummary(dto, inv);
        OperationInvoiceMapper.AppendAdjustmentLines(inv, dto);

        OperationInvoiceCalculator.RecalculateHeader(inv);
        await invoiceRepo.AddAsync(inv, cancellationToken);
        return OperationInvoiceMapper.ToDto(inv, 0);
    }
}
