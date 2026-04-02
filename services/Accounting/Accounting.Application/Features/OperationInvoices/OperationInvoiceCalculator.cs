using Accounting.Domain;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

namespace Accounting.Application.Features.OperationInvoices;

public static class OperationInvoiceCalculator
{
    public static void ApplyLineAmounts(OperationInvoiceLine line)
    {
        var disc = line.DiscountPercent;
        if (disc < 0) disc = 0;
        if (disc > 100) disc = 100;
        var net = Math.Round(line.Quantity * line.UnitPrice * (1 - disc / 100m), 2, MidpointRounding.AwayFromZero);
        line.LineNet = net;

        var exempt = Math.Round(Math.Max(0, line.TaxExemptionAmount), 2, MidpointRounding.AwayFromZero);
        if (exempt > net) exempt = net;
        var taxableNet = net - exempt;

        var vat = VatCalculation.VatAmountFromNet(taxableNet, line.VatPercent);
        var gross = Math.Round(taxableNet + vat, 2, MidpointRounding.AwayFromZero);
        line.LineVat = vat;
        line.LineGross = gross;
    }

    public static void RecalculateHeader(OperationInvoice inv)
    {
        decimal n = 0, v = 0, g = 0;
        var ordered = inv.Lines.OrderBy(x => x.SortOrder).ToList();
        foreach (var line in ordered)
        {
            ApplyLineAmounts(line);
            n += line.LineNet;
            v += line.LineVat;
            g += line.LineGross;
        }

        inv.SubtotalExclVat = Math.Round(n, 2, MidpointRounding.AwayFromZero);
        inv.VatTotal = Math.Round(v, 2, MidpointRounding.AwayFromZero);
        inv.TotalInclVat = Math.Round(g, 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>Preview totals from DTO lines without persisting.</summary>
    public static (decimal SubtotalExclVat, decimal VatTotal, decimal TotalInclVat, IReadOnlyList<LineAmounts> Lines) Preview(
        IReadOnlyList<InvoiceLineInput> lines)
    {
        var temp = new OperationInvoice { Id = Guid.Empty, Lines = new List<OperationInvoiceLine>() };
        var i = 0;
        foreach (var l in lines)
        {
            temp.Lines.Add(new OperationInvoiceLine
            {
                Id = Guid.Empty,
                SortOrder = i++,
                StockCode = l.StockCode,
                Description = l.Description,
                ValidUntil = l.ValidUntil,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice,
                DiscountPercent = l.DiscountPercent,
                VatPercent = l.VatPercent,
                TaxExemptionAmount = l.TaxExemptionAmount
            });
        }

        RecalculateHeader(temp);
        var previewLines = temp.Lines.OrderBy(x => x.SortOrder)
            .Select(x => new LineAmounts(x.SortOrder, x.LineNet, x.LineVat, x.LineGross))
            .ToList();
        return (temp.SubtotalExclVat, temp.VatTotal, temp.TotalInclVat, previewLines);
    }
}

public record InvoiceLineInput(
    string? StockCode,
    string Description,
    DateTime? ValidUntil,
    decimal Quantity,
    decimal UnitPrice,
    decimal DiscountPercent,
    decimal VatPercent,
    decimal TaxExemptionAmount);

public record LineAmounts(int SortOrder, decimal LineNet, decimal LineVat, decimal LineGross);
