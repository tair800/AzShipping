using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence.Repositories;

public class OperationInvoiceRepository(AccountingDbContext context) : IOperationInvoiceRepository
{
    public async Task<OperationInvoice?> GetByIdWithLinesAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.OperationInvoices
            .AsNoTracking()
            .Include(x => x.Lines)
            .Include(x => x.DiscountLines)
            .Include(x => x.TaxLines)
            .Include(x => x.NoteLines)
            .Include(x => x.PaymentLines)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<OperationInvoice?> GetByIdWithLinesTrackedAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.OperationInvoices
            .Include(x => x.Lines)
            .Include(x => x.DiscountLines)
            .Include(x => x.TaxLines)
            .Include(x => x.NoteLines)
            .Include(x => x.PaymentLines)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IReadOnlyList<OperationInvoice>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default)
        => await context.OperationInvoices
            .AsNoTracking()
            .Include(x => x.Lines)
            .Include(x => x.DiscountLines)
            .Include(x => x.TaxLines)
            .Include(x => x.NoteLines)
            .Include(x => x.PaymentLines)
            .Where(x => x.OperationId == operationId)
            .OrderByDescending(x => x.DocumentDate)
            .ThenByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<OperationInvoice>> GetAllForListAsync(CancellationToken cancellationToken = default)
        => await context.OperationInvoices
            .AsNoTracking()
            .OrderByDescending(x => x.DocumentDate)
            .ThenByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

    public async Task<OperationInvoice> AddAsync(OperationInvoice entity, CancellationToken cancellationToken = default)
    {
        context.OperationInvoices.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(OperationInvoice entity, CancellationToken cancellationToken = default)
    {
        var existing = await context.OperationInvoices
            .Include(x => x.Lines)
            .Include(x => x.DiscountLines)
            .Include(x => x.TaxLines)
            .Include(x => x.NoteLines)
            .Include(x => x.PaymentLines)
            .FirstOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
        if (existing == null)
            throw new InvalidOperationException($"Invoice {entity.Id} not found.");

        context.OperationInvoiceLines.RemoveRange(existing.Lines);
        context.Set<OperationInvoiceDiscountLine>().RemoveRange(existing.DiscountLines);
        context.Set<OperationInvoiceTaxLine>().RemoveRange(existing.TaxLines);
        context.Set<OperationInvoiceNoteLine>().RemoveRange(existing.NoteLines);
        context.Set<OperationInvoicePaymentLine>().RemoveRange(existing.PaymentLines);

        existing.OperationId = entity.OperationId;
        existing.PublicReference = entity.PublicReference;
        existing.InvoiceNumber = entity.InvoiceNumber;
        existing.DocumentDate = entity.DocumentDate;
        existing.IssueTime = entity.IssueTime;
        existing.DueDate = entity.DueDate;
        existing.PostponedDays = entity.PostponedDays;
        existing.InvoiceTypeCode = entity.InvoiceTypeCode;
        existing.InvoiceAccountCode = entity.InvoiceAccountCode;
        existing.ContractNumber = entity.ContractNumber;
        existing.InvoiceAddress = entity.InvoiceAddress;
        existing.InvoiceNote = entity.InvoiceNote;
        existing.ExpenseCenterCode = entity.ExpenseCenterCode;
        existing.SpecialCode = entity.SpecialCode;
        existing.ContractorName = entity.ContractorName;
        existing.PayerName = entity.PayerName;
        existing.PricingTypeCode = entity.PricingTypeCode;
        existing.BreakingRule = entity.BreakingRule;
        existing.WarehouseCode = entity.WarehouseCode;
        existing.HeadCode = entity.HeadCode;
        existing.DepartmentCode = entity.DepartmentCode;
        existing.LanguageCode = entity.LanguageCode;
        existing.TemplateCode = entity.TemplateCode;
        existing.CurrencyCode = entity.CurrencyCode;
        existing.Notes = entity.Notes;
        existing.IsSent = entity.IsSent;
        existing.PaymentsBalanceCurrency = entity.PaymentsBalanceCurrency;
        existing.PaymentsBalanceAmount = entity.PaymentsBalanceAmount;
        existing.SubtotalExclVat = entity.SubtotalExclVat;
        existing.VatTotal = entity.VatTotal;
        existing.TotalInclVat = entity.TotalInclVat;
        existing.UpdatedAtUtc = DateTime.UtcNow;

        foreach (var line in entity.Lines.OrderBy(l => l.SortOrder))
        {
            existing.Lines.Add(new OperationInvoiceLine
            {
                Id = Guid.NewGuid(),
                OperationInvoiceId = existing.Id,
                SortOrder = line.SortOrder,
                StockCode = line.StockCode,
                Description = line.Description,
                ValidUntil = line.ValidUntil,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
                DiscountPercent = line.DiscountPercent,
                VatPercent = line.VatPercent,
                TaxExemptionAmount = line.TaxExemptionAmount,
                LineNet = line.LineNet,
                LineVat = line.LineVat,
                LineGross = line.LineGross
            });
        }

        foreach (var line in entity.DiscountLines.OrderBy(l => l.SortOrder))
        {
            existing.DiscountLines.Add(new OperationInvoiceDiscountLine
            {
                Id = Guid.NewGuid(),
                OperationInvoiceId = existing.Id,
                SortOrder = line.SortOrder,
                TypeCode = line.TypeCode,
                Percent = line.Percent,
                Amount = line.Amount,
                AllowanceChargeReason = line.AllowanceChargeReason
            });
        }

        foreach (var line in entity.TaxLines.OrderBy(l => l.SortOrder))
        {
            existing.TaxLines.Add(new OperationInvoiceTaxLine
            {
                Id = Guid.NewGuid(),
                OperationInvoiceId = existing.Id,
                SortOrder = line.SortOrder,
                TaxableAmount = line.TaxableAmount,
                TaxTypeCode = line.TaxTypeCode,
                TaxPercent = line.TaxPercent,
                TaxAmount = line.TaxAmount,
                FinalAmount = line.FinalAmount,
                ExemptAmount = line.ExemptAmount,
                Rounding = line.Rounding,
                AccountCode = line.AccountCode
            });
        }

        foreach (var line in entity.NoteLines.OrderBy(l => l.SortOrder))
        {
            existing.NoteLines.Add(new OperationInvoiceNoteLine
            {
                Id = Guid.NewGuid(),
                OperationInvoiceId = existing.Id,
                SortOrder = line.SortOrder,
                CreatorDisplayName = line.CreatorDisplayName,
                NoteTypeCode = line.NoteTypeCode,
                NoteText = line.NoteText
            });
        }

        foreach (var line in entity.PaymentLines.OrderBy(l => l.SortOrder))
        {
            existing.PaymentLines.Add(new OperationInvoicePaymentLine
            {
                Id = Guid.NewGuid(),
                OperationInvoiceId = existing.Id,
                SortOrder = line.SortOrder,
                AppcardName = line.AppcardName,
                Amount = line.Amount,
                ConvertedAmount = line.ConvertedAmount,
                CurrencyCode = line.CurrencyCode,
                CurrencyRate = line.CurrencyRate,
                PersonName = line.PersonName
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.OperationInvoices.FindAsync([id], cancellationToken);
        if (e != null)
        {
            context.OperationInvoices.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
