using Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;
using Accounting.Domain.AggregatesModel.OperationActAggregate;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Accounting.Infrastructure.Persistence.Seed;

public static class AccountingDbSeeder
{
    /// <summary>Same as <c>OperationDbSeeder.DemoLogisticsOperationId1/2</c>.</summary>
    private static readonly Guid DemoOperationId1 = Guid.Parse("a1111111-1111-4111-8111-111111111101");
    private static readonly Guid DemoOperationId2 = Guid.Parse("a1111111-1111-4111-8111-111111111102");

    private static readonly (InvoiceLookupCategory Cat, string Code, string Name, int Sort)[] InvoiceLookupSeed =
    [
        (InvoiceLookupCategory.InvoiceType, "STD", "Standard AR invoice", 0),
        (InvoiceLookupCategory.InvoiceType, "PROFORMA", "Proforma / prepayment", 1),
        (InvoiceLookupCategory.InvoiceType, "CREDIT", "Credit note", 2),
        (InvoiceLookupCategory.InvoiceType, "DEBIT", "Debit note / surcharge", 3),
        (InvoiceLookupCategory.InvoiceAccount, "AR-MAIN", "Trade receivables — domestic", 0),
        (InvoiceLookupCategory.InvoiceAccount, "AR-INTL", "Trade receivables — international", 1),
        (InvoiceLookupCategory.InvoiceAccount, "AR-ADV", "Client advances / deposits", 2),
        (InvoiceLookupCategory.InvoiceAccount, "AR-SPOT", "Spot freight — misc debtors", 3),
        (InvoiceLookupCategory.ContractNumber, "CNT-2026-FWK", "Framework agreement 2026", 0),
        (InvoiceLookupCategory.ContractNumber, "CNT-SPOT", "Spot / transactional", 1),
        (InvoiceLookupCategory.ContractNumber, "CNT-FCL-OPS", "FCL operations tariff", 2),
        (InvoiceLookupCategory.ExpenseCenter, "EC-LOG", "Logistics & forwarding", 0),
        (InvoiceLookupCategory.ExpenseCenter, "EC-SALES", "Commercial / sales", 1),
        (InvoiceLookupCategory.ExpenseCenter, "EC-ADM", "Administration", 2),
        (InvoiceLookupCategory.SpecialCode, "NORM", "Standard VAT / regime", 0),
        (InvoiceLookupCategory.SpecialCode, "EXPORT", "Export / zero-rated", 1),
        (InvoiceLookupCategory.SpecialCode, "TRANSIT", "Transit / TIR regime", 2),
        (InvoiceLookupCategory.SpecialCode, "EXEMPT", "Statutory exemption", 3),
        (InvoiceLookupCategory.Contractor, "CARR-POOL", "Approved carrier pool", 0),
        (InvoiceLookupCategory.Contractor, "FWD-DEFAULT", "Default forwarder counterparty", 1),
        (InvoiceLookupCategory.Contractor, "LINE-GEN", "Shipping line — generic", 2),
        (InvoiceLookupCategory.Payer, "PAYER-CLIENT", "Bill to shipper / client account", 0),
        (InvoiceLookupCategory.Payer, "PAYER-PREPAID", "Prepaid — shipper funds", 1),
        (InvoiceLookupCategory.Payer, "PAYER-THIRD", "Third-party payer / notify party", 2),

        (InvoiceLookupCategory.InvoiceDiscountType, "ALLOWANCE", "Allowance / rebate", 0),
        (InvoiceLookupCategory.InvoiceDiscountType, "CASH", "Cash discount", 1),
        (InvoiceLookupCategory.InvoiceDiscountType, "VOL", "Volume discount", 2),

        (InvoiceLookupCategory.InvoiceNoteType, "INTERNAL", "Internal note", 0),
        (InvoiceLookupCategory.InvoiceNoteType, "CLIENT", "Client-visible note", 1),
        (InvoiceLookupCategory.InvoiceNoteType, "AUDIT", "Audit / compliance", 2),

        (InvoiceLookupCategory.InvoicePaymentAppcard, "POS-A", "POS terminal A", 0),
        (InvoiceLookupCategory.InvoicePaymentAppcard, "POS-B", "POS terminal B", 1),
        (InvoiceLookupCategory.InvoicePaymentAppcard, "BANK", "Bank transfer", 2),
    ];

    /// <summary>
    /// Invoice-specific reference data only. Department, pricing type, template, language, head, warehouse
    /// come from Settings.API (see invoice UI merge).
    /// Idempotent: adds missing (category, code) rows only — no early exit when the table is non-empty.
    /// </summary>
    public static async Task SeedInvoiceLookupsAsync(AccountingDbContext context, ILogger? logger,
        CancellationToken cancellationToken = default)
    {
        var existing = await context.InvoiceLookupOptions
            .Select(x => new { x.Category, x.Code })
            .ToListAsync(cancellationToken);
        var keys = new HashSet<(int Cat, string CodeLower)>(
            existing.Select(e => ((int)e.Category, e.Code.ToLowerInvariant())));

        var now = DateTime.UtcNow;
        var toAdd = new List<InvoiceLookupOption>();
        foreach (var (cat, code, name, sort) in InvoiceLookupSeed)
        {
            if (keys.Contains(((int)cat, code.ToLowerInvariant())))
                continue;
            toAdd.Add(new InvoiceLookupOption
            {
                Id = Guid.NewGuid(),
                Category = cat,
                Code = code,
                Name = name,
                SortOrder = sort,
                IsActive = true,
                CreatedAtUtc = now
            });
        }

        if (toAdd.Count == 0)
        {
            logger?.LogInformation("Invoice lookup seed skipped — all {Count} rows already present.", InvoiceLookupSeed.Length);
            return;
        }

        context.InvoiceLookupOptions.AddRange(toAdd);
        await context.SaveChangesAsync(cancellationToken);
        logger?.LogInformation("Seeded {Count} new invoice lookup options (idempotent).", toAdd.Count);
    }

    /// <summary>
    /// Sample AR invoices for demo operations. Idempotent per (OperationId, InvoiceNumber) so reruns fix partial
    /// seeds and cannot be blocked by a stray DEMO-INV-* row on another operation.
    /// </summary>
    public static async Task SeedSampleOperationInvoicesAsync(AccountingDbContext context, ILogger? logger,
        CancellationToken cancellationToken = default)
    {
        Task<bool> ExistsOnOpAsync(Guid operationId, string invoiceNumber)
            => context.OperationInvoices.AnyAsync(
                x => x.OperationId == operationId && x.InvoiceNumber == invoiceNumber, cancellationToken);

        var now = DateTime.UtcNow;
        var doc = DateOnly.FromDateTime(now).ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);
        var toAdd = new List<OperationInvoice>();

        if (!await ExistsOnOpAsync(DemoOperationId1, "DEMO-INV-1001"))
        {
        var inv1Id = Guid.NewGuid();
        var inv1 = new OperationInvoice
        {
            Id = inv1Id,
            OperationId = DemoOperationId1,
            PublicReference = "#DEMO-REF-001",
            InvoiceNumber = "DEMO-INV-1001",
            DocumentDate = doc,
            IssueTime = new TimeOnly(14, 30),
            DueDate = doc.AddDays(30),
            PostponedDays = 0,
            InvoiceTypeCode = "STD",
            InvoiceAccountCode = "AR-MAIN",
            ContractNumber = "CNT-SPOT",
            InvoiceAddress = "1 Nizami St, Baku, Azerbaijan",
            InvoiceNote = "Thank you for your business.",
            ExpenseCenterCode = "EC-LOG",
            SpecialCode = "NORM",
            ContractorName = "FWD-DEFAULT",
            PayerName = "PAYER-CLIENT",
            PricingTypeCode = "SPOT",
            BreakingRule = null,
            WarehouseCode = "BAKU",
            HeadCode = "HEAD-OPS",
            DepartmentCode = "FIN",
            LanguageCode = "en",
            TemplateCode = "TPL-STD",
            CurrencyCode = "USD",
            Notes = "Seed data — safe to delete.",
            IsSent = false,
            SubtotalExclVat = 800m,
            VatTotal = 144m,
            TotalInclVat = 944m,
            HeaderLineTotal = 800m,
            HeaderAdditions = 50m,
            HeaderDiscount = 40m,
            HeaderNetTotal = 810m,
            HeaderTaxTotal = 145.80m,
            HeaderTaxInclusiveTotal = 955.80m,
            HeaderVatExemption = 0m,
            HeaderStoppage = 0m,
            HeaderRounding = -0.05m,
            HeaderAmountInExchange = 0m,
            HeaderGeneralTotal = 955.75m,
            CreatedAtUtc = now,
        };

        var line1Id = Guid.NewGuid();
        var line2Id = Guid.NewGuid();
        inv1.Lines.Add(new OperationInvoiceLine
        {
            Id = line1Id,
            OperationInvoiceId = inv1Id,
            SortOrder = 0,
            StockCode = "SKU-AIR-01",
            Description = "Freight — main leg (demo)",
            Quantity = 2,
            UnitPrice = 250,
            DiscountPercent = 0,
            VatPercent = 18,
            TaxExemptionAmount = 0,
            LineNet = 500,
            LineVat = 90,
            LineGross = 590
        });
        inv1.Lines.Add(new OperationInvoiceLine
        {
            Id = line2Id,
            OperationInvoiceId = inv1Id,
            SortOrder = 1,
            StockCode = "SERV-HAND-1",
            Description = "Handling & documentation",
            Quantity = 1,
            UnitPrice = 300,
            DiscountPercent = 0,
            VatPercent = 18,
            TaxExemptionAmount = 0,
            LineNet = 300,
            LineVat = 54,
            LineGross = 354
        });

        inv1.DiscountLines.Add(new OperationInvoiceDiscountLine
        {
            Id = Guid.NewGuid(),
            OperationInvoiceId = inv1Id,
            SortOrder = 0,
            TypeCode = "ALLOWANCE",
            Percent = 0,
            Amount = 40,
            AllowanceChargeReason = "Promotional rebate (demo)"
        });

        inv1.TaxLines.Add(new OperationInvoiceTaxLine
        {
            Id = Guid.NewGuid(),
            OperationInvoiceId = inv1Id,
            SortOrder = 0,
            TaxableAmount = 800,
            TaxTypeCode = "VAT-SAL-18",
            TaxPercent = 18,
            TaxAmount = 144,
            FinalAmount = 944,
            ExemptAmount = 0,
            Rounding = null,
            AccountCode = "AR-MAIN"
        });

        inv1.NoteLines.Add(new OperationInvoiceNoteLine
        {
            Id = Guid.NewGuid(),
            OperationInvoiceId = inv1Id,
            SortOrder = 0,
            CreatorDisplayName = "Finance Bot",
            NoteTypeCode = "INTERNAL",
            NoteText = "Demo note: rates approved by management."
        });

        inv1.PaymentLines.Add(new OperationInvoicePaymentLine
        {
            Id = Guid.NewGuid(),
            OperationInvoiceId = inv1Id,
            SortOrder = 0,
            AppcardName = "BANK",
            Amount = 500,
            ConvertedAmount = 500,
            CurrencyCode = "USD",
            CurrencyRate = 1,
            PersonName = "A. Hasanova"
        });
        toAdd.Add(inv1);
        }

        if (!await ExistsOnOpAsync(DemoOperationId1, "DEMO-INV-1002"))
        {
        var inv2Id = Guid.NewGuid();
        var inv2 = new OperationInvoice
        {
            Id = inv2Id,
            OperationId = DemoOperationId1,
            PublicReference = "#DEMO-REF-002",
            InvoiceNumber = "DEMO-INV-1002",
            DocumentDate = doc,
            InvoiceTypeCode = "PROFORMA",
            InvoiceAccountCode = "AR-INTL",
            ContractNumber = "CNT-2026-FWK",
            InvoiceAddress = "Hoofdweg 12, Rotterdam, NL",
            InvoiceNote = "Proforma — wire when ready.",
            ExpenseCenterCode = "EC-SALES",
            SpecialCode = "EXPORT",
            ContractorName = "CARR-POOL",
            PayerName = "PAYER-PREPAID",
            PricingTypeCode = "CONTRACT",
            CurrencyCode = "USD",
            SubtotalExclVat = 1200m,
            VatTotal = 0m,
            TotalInclVat = 1200m,
            HeaderLineTotal = 1200m,
            HeaderNetTotal = 1200m,
            HeaderTaxTotal = 0m,
            HeaderTaxInclusiveTotal = 1200m,
            HeaderGeneralTotal = 1200m,
            CreatedAtUtc = now,
        };
        inv2.Lines.Add(new OperationInvoiceLine
        {
            Id = Guid.NewGuid(),
            OperationInvoiceId = inv2Id,
            SortOrder = 0,
            StockCode = "OCEAN-FCL",
            Description = "Ocean FCL slot allocation (demo)",
            Quantity = 1,
            UnitPrice = 1200,
            DiscountPercent = 0,
            VatPercent = 0,
            TaxExemptionAmount = 0,
            LineNet = 1200,
            LineVat = 0,
            LineGross = 1200
        });
        toAdd.Add(inv2);
        }

        if (!await ExistsOnOpAsync(DemoOperationId2, "DEMO-INV-2001"))
        {
        var inv3Id = Guid.NewGuid();
        var inv3 = new OperationInvoice
        {
            Id = inv3Id,
            OperationId = DemoOperationId2,
            InvoiceNumber = "DEMO-INV-2001",
            DocumentDate = doc,
            InvoiceTypeCode = "STD",
            InvoiceAccountCode = "AR-MAIN",
            CurrencyCode = "EUR",
            PayerName = "PAYER-CLIENT",
            ContractorName = "LINE-GEN",
            SubtotalExclVat = 2360m,
            VatTotal = 424.80m,
            TotalInclVat = 2784.80m,
            HeaderLineTotal = 2360m,
            HeaderNetTotal = 2360m,
            HeaderTaxTotal = 424.80m,
            HeaderTaxInclusiveTotal = 2784.80m,
            HeaderGeneralTotal = 2784.80m,
            CreatedAtUtc = now,
        };
        inv3.Lines.Add(new OperationInvoiceLine
        {
            Id = Guid.NewGuid(),
            OperationInvoiceId = inv3Id,
            SortOrder = 0,
            Description = "Road + customs bundle (demo)",
            Quantity = 4,
            UnitPrice = 590,
            DiscountPercent = 0,
            VatPercent = 18,
            TaxExemptionAmount = 0,
            LineNet = 2360,
            LineVat = 424.80m,
            LineGross = 2784.80m
        });
        toAdd.Add(inv3);
        }

        if (toAdd.Count == 0)
        {
            logger?.LogInformation("Sample operation invoices already present on demo operations; skipped.");
            return;
        }

        context.OperationInvoices.AddRange(toAdd);
        await context.SaveChangesAsync(cancellationToken);
        logger?.LogInformation("Seeded {Count} sample operation invoice(s) for demo operations.", toAdd.Count);
    }

    /// <summary>
    /// Sample acts for the accounting act list UI. Idempotent by <see cref="OperationAct.ActNo"/> prefix DEMO-ACT-*.
    /// </summary>
    public static async Task SeedSampleOperationActsAsync(AccountingDbContext context, ILogger? logger,
        CancellationToken cancellationToken = default)
    {
        if (await context.OperationActs.AnyAsync(a => a.ActNo == "DEMO-ACT-1001", cancellationToken))
        {
            logger?.LogInformation("Sample operation acts already present; skipped.");
            return;
        }

        var inv1 = await context.OperationInvoices
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.OperationId == DemoOperationId1 && x.InvoiceNumber == "DEMO-INV-1001",
                cancellationToken);

        var invDisplayNo = inv1 == null
            ? "1001"
            : inv1.InvoiceNumber.Replace("DEMO-INV-", "", StringComparison.OrdinalIgnoreCase).Trim();

        var doc = DateOnly.FromDateTime(DateTime.UtcNow);
        var acts = new List<OperationAct>
        {
            new()
            {
                OperationInvoiceId = inv1?.Id,
                Payer = "Demo Client LLC",
                OrderNo = "AZ-ORD-1001",
                OrderDate = doc.AddDays(-20),
                ActNo = "DEMO-ACT-1001",
                ActDischargeDate = doc.AddDays(-5),
                ActSumWithoutVatAmount = 5632.06m,
                ActSumWithoutVatCurrency = "USD",
                ActSumWithVatAmount = 6645.83m,
                ActSumWithVatCurrency = "USD",
                InvoiceNo = $"AZ-INV-{invDisplayNo}",
                ActInvoiceDate = doc.AddDays(-3),
                ActInvoiceSumWithoutVatAmount = 9580.00m,
                ActInvoiceSumWithoutVatCurrency = "AZN",
                ActInvoiceSumWithVatAmount = 11304.40m,
                ActInvoiceSumWithVatCurrency = "AZN",
                BasicCurrencyWithoutVatAmount = 5632.06m,
                BasicCurrencyWithoutVatCurrency = "USD",
                BasicCurrencyWithVatAmount = 6645.83m,
                BasicCurrencyWithVatCurrency = "USD",
                BalancePaidAmount = 0m,
                BalanceTotalAmount = 7667.00m,
                BalanceCurrency = "AZN",
                SortOrder = 0,
            },
            new()
            {
                Payer = "Rotterdam Freight BV",
                OrderNo = "AZ-ORD-1002",
                OrderDate = doc.AddDays(-14),
                ActNo = "DEMO-ACT-1002",
                ActDischargeDate = doc.AddDays(-2),
                ActSumWithoutVatAmount = 1200.00m,
                ActSumWithoutVatCurrency = "USD",
                ActSumWithVatAmount = 1200.00m,
                ActSumWithVatCurrency = "USD",
                InvoiceNo = "AZ-INV-DEMO-1002",
                ActInvoiceDate = doc,
                ActInvoiceSumWithoutVatAmount = 2040.00m,
                ActInvoiceSumWithoutVatCurrency = "AZN",
                ActInvoiceSumWithVatAmount = 2040.00m,
                ActInvoiceSumWithVatCurrency = "AZN",
                BasicCurrencyWithoutVatAmount = 1200.00m,
                BasicCurrencyWithoutVatCurrency = "USD",
                BasicCurrencyWithVatAmount = 1200.00m,
                BasicCurrencyWithVatCurrency = "USD",
                BalancePaidAmount = 500.00m,
                BalanceTotalAmount = 2040.00m,
                BalanceCurrency = "AZN",
                SortOrder = 1,
            },
        };

        context.OperationActs.AddRange(acts);
        await context.SaveChangesAsync(cancellationToken);
        logger?.LogInformation("Seeded {Count} sample operation act(s).", acts.Count);
    }

    public static async Task SeedAsync(AccountingDbContext context, ILogger? logger, CancellationToken cancellationToken = default)
    {
        if (await context.VatDefinitions.AnyAsync(cancellationToken))
            return;

        var now = DateTime.UtcNow;
        var rows = new List<VatDefinition>
        {
            new()
            {
                Id = Guid.NewGuid(),
                CreatedAtUtc = now,
                Name = "0%",
                Percent = 0,
                IsAlcohol = false,
                BuyingAccountCode = "VAT-PUR-0",
                SellingAccountCode = "VAT-SAL-0",
                IsActive = true,
            },
            new()
            {
                Id = Guid.NewGuid(),
                CreatedAtUtc = now,
                Name = "18% ƏDV",
                Percent = 18,
                IsAlcohol = false,
                BuyingAccountCode = "VAT-PUR-18",
                SellingAccountCode = "VAT-SAL-18",
                IsActive = true,
            },
            new()
            {
                Id = Guid.NewGuid(),
                CreatedAtUtc = now,
                Name = "21%",
                Percent = 21,
                IsAlcohol = false,
                BuyingAccountCode = "VAT-PUR-21",
                SellingAccountCode = "VAT-SAL-21",
                IsActive = true,
            },
            new()
            {
                Id = Guid.NewGuid(),
                CreatedAtUtc = now,
                Name = "Without VAT",
                Percent = 0,
                IsAlcohol = false,
                BuyingAccountCode = "VAT-PUR-EX",
                SellingAccountCode = "VAT-SAL-EX",
                IsActive = true,
            },
        };
        context.VatDefinitions.AddRange(rows);
        await context.SaveChangesAsync(cancellationToken);
        logger?.LogInformation("Seeded {Count} VAT definitions.", rows.Count);
    }
}
