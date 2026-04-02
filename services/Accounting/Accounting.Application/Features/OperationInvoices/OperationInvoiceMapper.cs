using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

namespace Accounting.Application.Features.OperationInvoices;

public static class OperationInvoiceMapper
{
    private static string? T(string? s, int maxLen)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;
        s = s.Trim();
        return s.Length <= maxLen ? s : s[..maxLen];
    }

    /// <summary>Maps editable header fields from API DTO to domain entity (create or update body).</summary>
    public static void ApplyHeader(CreateOperationInvoiceDto dto, OperationInvoice inv, string invoiceNumber,
        string currencyCodeUpper)
    {
        inv.InvoiceNumber = invoiceNumber;
        inv.PublicReference = T(dto.PublicReference, 40);
        inv.DocumentDate = dto.DocumentDate;
        inv.IssueTime = dto.IssueTime.HasValue ? TimeOnly.FromTimeSpan(dto.IssueTime.Value) : null;
        inv.DueDate = dto.DueDate;
        inv.PostponedDays = dto.PostponedDays;
        inv.InvoiceTypeCode = T(dto.InvoiceTypeCode, 80);
        inv.InvoiceAccountCode = T(dto.InvoiceAccountCode, 120);
        inv.ContractNumber = T(dto.ContractNumber, 120);
        inv.InvoiceAddress = T(dto.InvoiceAddress, 2000);
        inv.InvoiceNote = T(dto.InvoiceNote, 4000);
        inv.ExpenseCenterCode = T(dto.ExpenseCenterCode, 120);
        inv.SpecialCode = T(dto.SpecialCode, 120);
        inv.ContractorName = T(dto.ContractorName, 300);
        inv.PayerName = T(dto.PayerName, 300);
        inv.PricingTypeCode = T(dto.PricingTypeCode, 80);
        inv.BreakingRule = T(dto.BreakingRule, 200);
        inv.WarehouseCode = T(dto.WarehouseCode, 120);
        inv.HeadCode = T(dto.HeadCode, 120);
        inv.DepartmentCode = T(dto.DepartmentCode, 120);
        inv.LanguageCode = T(dto.LanguageCode, 16);
        inv.TemplateCode = T(dto.TemplateCode, 120);
        inv.CurrencyCode = currencyCodeUpper;
        inv.Notes = T(dto.Notes, 4000);
        inv.IsSent = dto.IsSent;
        var pbc = T(dto.PaymentsBalanceCurrency, 8);
        inv.PaymentsBalanceCurrency = string.IsNullOrEmpty(pbc) ? null : pbc.ToUpperInvariant();
        inv.PaymentsBalanceAmount = dto.PaymentsBalanceAmount;
    }

    public static void ApplySummary(CreateOperationInvoiceDto dto, OperationInvoice inv)
    {
        inv.HeaderLineTotal = dto.HeaderLineTotal;
        inv.HeaderAdditions = dto.HeaderAdditions;
        inv.HeaderDiscount = dto.HeaderDiscount;
        inv.HeaderNetTotal = dto.HeaderNetTotal;
        inv.HeaderTaxTotal = dto.HeaderTaxTotal;
        inv.HeaderTaxInclusiveTotal = dto.HeaderTaxInclusiveTotal;
        inv.HeaderVatExemption = dto.HeaderVatExemption;
        inv.HeaderStoppage = dto.HeaderStoppage;
        inv.HeaderRounding = dto.HeaderRounding;
        inv.HeaderAmountInExchange = dto.HeaderAmountInExchange;
        inv.HeaderGeneralTotal = dto.HeaderGeneralTotal;
    }

    public static OperationInvoiceLine ToLineEntity(CreateOperationInvoiceLineDto l, int sortOrder) => new()
    {
        Id = Guid.NewGuid(),
        SortOrder = sortOrder,
        StockCode = T(l.StockCode, 200),
        Description = string.IsNullOrWhiteSpace(l.Description) ? "—" : l.Description.Trim(),
        ValidUntil = l.ValidUntil,
        Quantity = l.Quantity,
        UnitPrice = l.UnitPrice,
        DiscountPercent = l.DiscountPercent,
        VatPercent = l.VatPercent,
        TaxExemptionAmount = l.TaxExemptionAmount
    };

    public static void AppendAdjustmentLines(OperationInvoice inv, CreateOperationInvoiceDto dto)
    {
        var di = 0;
        foreach (var l in dto.DiscountLines ?? Array.Empty<CreateOperationInvoiceDiscountLineDto>())
            inv.DiscountLines.Add(ToDiscountLineEntity(l, inv.Id, di++));

        var ti = 0;
        foreach (var l in dto.TaxLines ?? Array.Empty<CreateOperationInvoiceTaxLineDto>())
            inv.TaxLines.Add(ToTaxLineEntity(l, inv.Id, ti++));

        var ni = 0;
        foreach (var l in dto.NoteLines ?? Array.Empty<CreateOperationInvoiceNoteLineDto>())
            inv.NoteLines.Add(ToNoteLineEntity(l, inv.Id, ni++));

        var pi = 0;
        foreach (var l in dto.PaymentLines ?? Array.Empty<CreateOperationInvoicePaymentLineDto>())
            inv.PaymentLines.Add(ToPaymentLineEntity(l, inv.Id, pi++));
    }

    public static OperationInvoiceDiscountLine ToDiscountLineEntity(CreateOperationInvoiceDiscountLineDto l,
        Guid invoiceId, int sortOrder) => new()
    {
        Id = Guid.NewGuid(),
        OperationInvoiceId = invoiceId,
        SortOrder = sortOrder,
        TypeCode = T(l.TypeCode, 80),
        Percent = l.Percent,
        Amount = l.Amount,
        AllowanceChargeReason = T(l.AllowanceChargeReason, 500)
    };

    public static OperationInvoiceTaxLine ToTaxLineEntity(CreateOperationInvoiceTaxLineDto l,
        Guid invoiceId, int sortOrder) => new()
    {
        Id = Guid.NewGuid(),
        OperationInvoiceId = invoiceId,
        SortOrder = sortOrder,
        TaxableAmount = l.TaxableAmount,
        TaxTypeCode = T(l.TaxTypeCode, 120),
        TaxPercent = l.TaxPercent,
        TaxAmount = l.TaxAmount,
        FinalAmount = l.FinalAmount,
        ExemptAmount = l.ExemptAmount,
        Rounding = l.Rounding,
        AccountCode = T(l.AccountCode, 120)
    };

    public static OperationInvoiceNoteLine ToNoteLineEntity(CreateOperationInvoiceNoteLineDto l,
        Guid invoiceId, int sortOrder) => new()
    {
        Id = Guid.NewGuid(),
        OperationInvoiceId = invoiceId,
        SortOrder = sortOrder,
        CreatorDisplayName = T(l.CreatorDisplayName, 300),
        NoteTypeCode = T(l.NoteTypeCode, 80),
        NoteText = T(l.NoteText, 4000) ?? string.Empty
    };

    public static OperationInvoicePaymentLine ToPaymentLineEntity(CreateOperationInvoicePaymentLineDto l,
        Guid invoiceId, int sortOrder) => new()
    {
        Id = Guid.NewGuid(),
        OperationInvoiceId = invoiceId,
        SortOrder = sortOrder,
        AppcardName = T(l.AppcardName, 120),
        Amount = l.Amount,
        ConvertedAmount = l.ConvertedAmount,
        CurrencyCode = T(l.CurrencyCode, 8),
        CurrencyRate = l.CurrencyRate,
        PersonName = T(l.PersonName, 300)
    };

    public static OperationInvoiceLineDto ToLineDto(OperationInvoiceLine x)
    {
        var net = x.LineNet;
        var gross = x.LineGross;
        return new OperationInvoiceLineDto
        {
            Id = x.Id,
            SortOrder = x.SortOrder,
            StockCode = x.StockCode,
            Description = x.Description,
            ValidUntil = x.ValidUntil,
            Quantity = x.Quantity,
            UnitPrice = x.UnitPrice,
            DiscountPercent = x.DiscountPercent,
            VatPercent = x.VatPercent,
            LineTotalExVat = net,
            LineNet = net,
            LineVat = x.LineVat,
            TaxInclusiveTotal = gross,
            LineGross = gross,
            TaxExemptionAmount = x.TaxExemptionAmount
        };
    }

    public static OperationInvoiceDto ToDto(OperationInvoice e, decimal paidInInvoiceCurrency)
    {
        var paid = Math.Round(paidInInvoiceCurrency, 2, MidpointRounding.AwayFromZero);
        var total = Math.Round(e.TotalInclVat, 2, MidpointRounding.AwayFromZero);
        var outstanding = Math.Round(total - paid, 2, MidpointRounding.AwayFromZero);
        return new OperationInvoiceDto
        {
            Id = e.Id,
            OperationId = e.OperationId,
            PublicReference = e.PublicReference,
            InvoiceNumber = e.InvoiceNumber,
            DocumentDate = e.DocumentDate,
            IssueTime = e.IssueTime.HasValue ? e.IssueTime.Value.ToTimeSpan() : null,
            DueDate = e.DueDate,
            PostponedDays = e.PostponedDays,
            InvoiceTypeCode = e.InvoiceTypeCode,
            InvoiceAccountCode = e.InvoiceAccountCode,
            ContractNumber = e.ContractNumber,
            InvoiceAddress = e.InvoiceAddress,
            InvoiceNote = e.InvoiceNote,
            ExpenseCenterCode = e.ExpenseCenterCode,
            SpecialCode = e.SpecialCode,
            ContractorName = e.ContractorName,
            PayerName = e.PayerName,
            PricingTypeCode = e.PricingTypeCode,
            BreakingRule = e.BreakingRule,
            WarehouseCode = e.WarehouseCode,
            HeadCode = e.HeadCode,
            DepartmentCode = e.DepartmentCode,
            LanguageCode = e.LanguageCode,
            TemplateCode = e.TemplateCode,
            CurrencyCode = e.CurrencyCode,
            Notes = e.Notes,
            IsSent = e.IsSent,
            SubtotalExclVat = e.SubtotalExclVat,
            VatTotal = e.VatTotal,
            TotalInclVat = e.TotalInclVat,
            PaymentsBalanceCurrency = e.PaymentsBalanceCurrency,
            PaymentsBalanceAmount = e.PaymentsBalanceAmount,
            CreatedAtUtc = e.CreatedAtUtc,
            UpdatedAtUtc = e.UpdatedAtUtc,
            PaidAmountInInvoiceCurrency = paid,
            OutstandingInInvoiceCurrency = outstanding,
            HeaderLineTotal = e.HeaderLineTotal,
            HeaderAdditions = e.HeaderAdditions,
            HeaderDiscount = e.HeaderDiscount,
            HeaderNetTotal = e.HeaderNetTotal,
            HeaderTaxTotal = e.HeaderTaxTotal,
            HeaderTaxInclusiveTotal = e.HeaderTaxInclusiveTotal,
            HeaderVatExemption = e.HeaderVatExemption,
            HeaderStoppage = e.HeaderStoppage,
            HeaderRounding = e.HeaderRounding,
            HeaderAmountInExchange = e.HeaderAmountInExchange,
            HeaderGeneralTotal = e.HeaderGeneralTotal,
            Lines = e.Lines.OrderBy(l => l.SortOrder).Select(ToLineDto).ToList(),
            DiscountLines = e.DiscountLines.OrderBy(l => l.SortOrder).Select(ToDiscountLineDto).ToList(),
            TaxLines = e.TaxLines.OrderBy(l => l.SortOrder).Select(ToTaxLineDto).ToList(),
            NoteLines = e.NoteLines.OrderBy(l => l.SortOrder).Select(ToNoteLineDto).ToList(),
            PaymentLines = e.PaymentLines.OrderBy(l => l.SortOrder).Select(ToPaymentLineDto).ToList()
        };
    }

    public static OperationInvoiceListItemDto ToListItemDto(OperationInvoice e, decimal paidInInvoiceCurrency)
    {
        var paid = Math.Round(paidInInvoiceCurrency, 2, MidpointRounding.AwayFromZero);
        var total = Math.Round(e.TotalInclVat, 2, MidpointRounding.AwayFromZero);
        var outstanding = Math.Round(total - paid, 2, MidpointRounding.AwayFromZero);
        return new OperationInvoiceListItemDto
        {
            Id = e.Id,
            OperationId = e.OperationId,
            PublicReference = e.PublicReference,
            InvoiceNumber = e.InvoiceNumber,
            DocumentDate = e.DocumentDate,
            PayerName = e.PayerName,
            InvoiceTypeCode = e.InvoiceTypeCode,
            CurrencyCode = e.CurrencyCode,
            SubtotalExclVat = e.SubtotalExclVat,
            VatTotal = e.VatTotal,
            TotalInclVat = e.TotalInclVat,
            PaidAmountInInvoiceCurrency = paid,
            OutstandingInInvoiceCurrency = outstanding,
            IsSent = e.IsSent,
        };
    }

    public static OperationInvoiceDiscountLineDto ToDiscountLineDto(OperationInvoiceDiscountLine x) => new()
    {
        Id = x.Id,
        SortOrder = x.SortOrder,
        TypeCode = x.TypeCode,
        Percent = x.Percent,
        Amount = x.Amount,
        AllowanceChargeReason = x.AllowanceChargeReason
    };

    public static OperationInvoiceTaxLineDto ToTaxLineDto(OperationInvoiceTaxLine x) => new()
    {
        Id = x.Id,
        SortOrder = x.SortOrder,
        TaxableAmount = x.TaxableAmount,
        TaxTypeCode = x.TaxTypeCode,
        TaxPercent = x.TaxPercent,
        TaxAmount = x.TaxAmount,
        FinalAmount = x.FinalAmount,
        ExemptAmount = x.ExemptAmount,
        Rounding = x.Rounding,
        AccountCode = x.AccountCode
    };

    public static OperationInvoiceNoteLineDto ToNoteLineDto(OperationInvoiceNoteLine x) => new()
    {
        Id = x.Id,
        SortOrder = x.SortOrder,
        CreatorDisplayName = x.CreatorDisplayName,
        NoteTypeCode = x.NoteTypeCode,
        NoteText = x.NoteText
    };

    public static OperationInvoicePaymentLineDto ToPaymentLineDto(OperationInvoicePaymentLine x) => new()
    {
        Id = x.Id,
        SortOrder = x.SortOrder,
        AppcardName = x.AppcardName,
        Amount = x.Amount,
        ConvertedAmount = x.ConvertedAmount,
        CurrencyCode = x.CurrencyCode,
        CurrencyRate = x.CurrencyRate,
        PersonName = x.PersonName
    };
}
