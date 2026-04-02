using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Application.Features.OperationInvoices;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Calculate;

public class CalculateOperationInvoiceCommandHandler
    : IRequestHandler<CalculateOperationInvoiceCommand, CalculateOperationInvoiceResponseDto>
{
    public Task<CalculateOperationInvoiceResponseDto> Handle(CalculateOperationInvoiceCommand request,
        CancellationToken cancellationToken)
    {
        var lines = request.Body.Lines ?? [];
        var inputs = lines.Select(l => new InvoiceLineInput(
            string.IsNullOrWhiteSpace(l.StockCode) ? null : l.StockCode.Trim(),
            string.IsNullOrWhiteSpace(l.Description) ? "—" : l.Description.Trim(),
            l.ValidUntil,
            l.Quantity,
            l.UnitPrice,
            l.DiscountPercent,
            l.VatPercent,
            l.TaxExemptionAmount)).ToList();

        var (sub, vat, gross, previewLines) = OperationInvoiceCalculator.Preview(inputs);
        var dtoLines = previewLines.Zip(inputs, (amt, inp) => new CalculatedLineDto
        {
            SortOrder = amt.SortOrder,
            StockCode = inp.StockCode,
            Description = inp.Description,
            ValidUntil = inp.ValidUntil,
            Quantity = inp.Quantity,
            UnitPrice = inp.UnitPrice,
            DiscountPercent = inp.DiscountPercent,
            VatPercent = inp.VatPercent,
            TaxExemptionAmount = inp.TaxExemptionAmount,
            LineTotalExVat = amt.LineNet,
            LineNet = amt.LineNet,
            LineVat = amt.LineVat,
            TaxInclusiveTotal = amt.LineGross,
            LineGross = amt.LineGross
        }).ToList();

        return Task.FromResult(new CalculateOperationInvoiceResponseDto
        {
            SubtotalExclVat = sub,
            VatTotal = vat,
            TotalInclVat = gross,
            Lines = dtoLines
        });
    }
}
