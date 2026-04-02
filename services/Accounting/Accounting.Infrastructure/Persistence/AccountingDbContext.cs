using Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;
using Accounting.Domain.AggregatesModel.OperationActAggregate;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Persistence;

public class AccountingDbContext : DbContext
{
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options) { }

    public DbSet<Payment> Payments { get; set; }
    public DbSet<VatDefinition> VatDefinitions { get; set; }
    public DbSet<OperationInvoice> OperationInvoices { get; set; }
    public DbSet<OperationInvoiceLine> OperationInvoiceLines { get; set; }
    public DbSet<InvoiceLookupOption> InvoiceLookupOptions { get; set; }
    public DbSet<OperationAct> OperationActs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OperationInvoice>(e =>
        {
            e.ToTable("OperationInvoices");
            e.HasKey(x => x.Id);
            e.Property(x => x.PublicReference).HasMaxLength(40);
            e.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(120);
            e.Property(x => x.IssueTime);
            e.Property(x => x.InvoiceTypeCode).HasMaxLength(80);
            e.Property(x => x.InvoiceAccountCode).HasMaxLength(120);
            e.Property(x => x.ContractNumber).HasMaxLength(120);
            e.Property(x => x.InvoiceAddress).HasMaxLength(2000);
            e.Property(x => x.InvoiceNote).HasMaxLength(4000);
            e.Property(x => x.ExpenseCenterCode).HasMaxLength(120);
            e.Property(x => x.SpecialCode).HasMaxLength(120);
            e.Property(x => x.ContractorName).HasMaxLength(300);
            e.Property(x => x.PayerName).HasMaxLength(300);
            e.Property(x => x.PricingTypeCode).HasMaxLength(80);
            e.Property(x => x.BreakingRule).HasMaxLength(200);
            e.Property(x => x.WarehouseCode).HasMaxLength(120);
            e.Property(x => x.HeadCode).HasMaxLength(120);
            e.Property(x => x.DepartmentCode).HasMaxLength(120);
            e.Property(x => x.LanguageCode).HasMaxLength(16);
            e.Property(x => x.TemplateCode).HasMaxLength(120);
            e.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(8);
            e.Property(x => x.Notes).HasMaxLength(4000);
            e.Property(x => x.PaymentsBalanceCurrency).HasMaxLength(8);
            e.Property(x => x.SubtotalExclVat).HasPrecision(18, 2);
            e.Property(x => x.VatTotal).HasPrecision(18, 2);
            e.Property(x => x.TotalInclVat).HasPrecision(18, 2);
            e.Property(x => x.PaymentsBalanceAmount).HasPrecision(18, 2);
            e.Property(x => x.HeaderLineTotal).HasPrecision(18, 2);
            e.Property(x => x.HeaderAdditions).HasPrecision(18, 2);
            e.Property(x => x.HeaderDiscount).HasPrecision(18, 2);
            e.Property(x => x.HeaderNetTotal).HasPrecision(18, 2);
            e.Property(x => x.HeaderTaxTotal).HasPrecision(18, 2);
            e.Property(x => x.HeaderTaxInclusiveTotal).HasPrecision(18, 2);
            e.Property(x => x.HeaderVatExemption).HasPrecision(18, 2);
            e.Property(x => x.HeaderStoppage).HasPrecision(18, 2);
            e.Property(x => x.HeaderRounding).HasPrecision(18, 2);
            e.Property(x => x.HeaderAmountInExchange).HasPrecision(18, 2);
            e.Property(x => x.HeaderGeneralTotal).HasPrecision(18, 2);
            e.HasMany(x => x.Lines)
                .WithOne(x => x.OperationInvoice)
                .HasForeignKey(x => x.OperationInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.DiscountLines)
                .WithOne(x => x.OperationInvoice)
                .HasForeignKey(x => x.OperationInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.TaxLines)
                .WithOne(x => x.OperationInvoice)
                .HasForeignKey(x => x.OperationInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.NoteLines)
                .WithOne(x => x.OperationInvoice)
                .HasForeignKey(x => x.OperationInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasMany(x => x.PaymentLines)
                .WithOne(x => x.OperationInvoice)
                .HasForeignKey(x => x.OperationInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OperationInvoiceLine>(e =>
        {
            e.ToTable("OperationInvoiceLines");
            e.HasKey(x => x.Id);
            e.Property(x => x.StockCode).HasMaxLength(200);
            e.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            e.Property(x => x.Quantity).HasPrecision(18, 4);
            e.Property(x => x.UnitPrice).HasPrecision(18, 4);
            e.Property(x => x.DiscountPercent).HasPrecision(8, 4);
            e.Property(x => x.VatPercent).HasPrecision(8, 4);
            e.Property(x => x.TaxExemptionAmount).HasPrecision(18, 2);
            e.Property(x => x.LineNet).HasPrecision(18, 2);
            e.Property(x => x.LineVat).HasPrecision(18, 2);
            e.Property(x => x.LineGross).HasPrecision(18, 2);
        });

        modelBuilder.Entity<OperationInvoiceDiscountLine>(e =>
        {
            e.ToTable("OperationInvoiceDiscountLines");
            e.HasKey(x => x.Id);
            e.Property(x => x.TypeCode).HasMaxLength(80);
            e.Property(x => x.Percent).HasPrecision(8, 4);
            e.Property(x => x.Amount).HasPrecision(18, 2);
            e.Property(x => x.AllowanceChargeReason).HasMaxLength(500);
        });

        modelBuilder.Entity<OperationInvoiceTaxLine>(e =>
        {
            e.ToTable("OperationInvoiceTaxLines");
            e.HasKey(x => x.Id);
            e.Property(x => x.TaxableAmount).HasPrecision(18, 2);
            e.Property(x => x.TaxTypeCode).HasMaxLength(120);
            e.Property(x => x.TaxPercent).HasPrecision(8, 4);
            e.Property(x => x.TaxAmount).HasPrecision(18, 2);
            e.Property(x => x.FinalAmount).HasPrecision(18, 2);
            e.Property(x => x.ExemptAmount).HasPrecision(18, 2);
            e.Property(x => x.Rounding).HasPrecision(18, 2);
            e.Property(x => x.AccountCode).HasMaxLength(120);
        });

        modelBuilder.Entity<OperationInvoiceNoteLine>(e =>
        {
            e.ToTable("OperationInvoiceNoteLines");
            e.HasKey(x => x.Id);
            e.Property(x => x.CreatorDisplayName).HasMaxLength(300);
            e.Property(x => x.NoteTypeCode).HasMaxLength(80);
            e.Property(x => x.NoteText).IsRequired().HasMaxLength(4000);
        });

        modelBuilder.Entity<OperationInvoicePaymentLine>(e =>
        {
            e.ToTable("OperationInvoicePaymentLines");
            e.HasKey(x => x.Id);
            e.Property(x => x.AppcardName).HasMaxLength(120);
            e.Property(x => x.Amount).HasPrecision(18, 2);
            e.Property(x => x.ConvertedAmount).HasPrecision(18, 2);
            e.Property(x => x.CurrencyCode).HasMaxLength(8);
            e.Property(x => x.CurrencyRate).HasPrecision(18, 6);
            e.Property(x => x.PersonName).HasMaxLength(300);
        });

        modelBuilder.Entity<Payment>(e =>
        {
            e.ToTable("Payments");
            e.HasKey(x => x.Id);
            e.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(8);
            e.Property(x => x.PaidAmount).HasPrecision(18, 2);
            e.Property(x => x.PaymentMethod).HasConversion<int>();
            e.Property(x => x.Direction).HasConversion<int>();
            e.Property(x => x.ReceivedBy).HasMaxLength(300);
            e.Property(x => x.Notes).HasMaxLength(4000);
            e.Property(x => x.OrderNo).HasMaxLength(80);
            e.Property(x => x.AccountLabel).HasMaxLength(100);
            e.Property(x => x.InvoiceReference).HasMaxLength(120);
            e.Property(x => x.CounterpartyName).HasMaxLength(300);
            e.HasOne<OperationInvoice>()
                .WithMany()
                .HasForeignKey(x => x.OperationInvoiceId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<VatDefinition>(e =>
        {
            e.ToTable("VatDefinitions");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Percent).HasPrecision(8, 4);
            e.Property(x => x.BuyingAccountName).HasMaxLength(200);
            e.Property(x => x.BuyingAccountCode).IsRequired().HasMaxLength(80);
            e.Property(x => x.SellingAccountName).HasMaxLength(200);
            e.Property(x => x.SellingAccountCode).IsRequired().HasMaxLength(80);
            e.Property(x => x.Notes).HasMaxLength(4000);
        });

        modelBuilder.Entity<InvoiceLookupOption>(e =>
        {
            e.ToTable("InvoiceLookupOptions");
            e.HasKey(x => x.Id);
            e.Property(x => x.Category).HasConversion<int>();
            e.Property(x => x.Code).IsRequired().HasMaxLength(80);
            e.Property(x => x.Name).IsRequired().HasMaxLength(300);
            e.HasIndex(x => new { x.Category, x.Code }).IsUnique();
        });

        modelBuilder.Entity<OperationAct>(e =>
        {
            e.ToTable("OperationActs");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Payer).IsRequired().HasMaxLength(300);
            e.Property(x => x.OrderNo).IsRequired().HasMaxLength(120);
            e.Property(x => x.ActNo).IsRequired().HasMaxLength(120);
            e.Property(x => x.InvoiceNo).IsRequired().HasMaxLength(120);
            e.Property(x => x.ActSumWithoutVatCurrency).HasMaxLength(8);
            e.Property(x => x.ActSumWithVatCurrency).HasMaxLength(8);
            e.Property(x => x.ActInvoiceSumWithoutVatCurrency).HasMaxLength(8);
            e.Property(x => x.ActInvoiceSumWithVatCurrency).HasMaxLength(8);
            e.Property(x => x.BasicCurrencyWithoutVatCurrency).HasMaxLength(8);
            e.Property(x => x.BasicCurrencyWithVatCurrency).HasMaxLength(8);
            e.Property(x => x.BalanceCurrency).HasMaxLength(8);
            e.Property(x => x.ActSumWithoutVatAmount).HasPrecision(18, 2);
            e.Property(x => x.ActSumWithVatAmount).HasPrecision(18, 2);
            e.Property(x => x.ActInvoiceSumWithoutVatAmount).HasPrecision(18, 2);
            e.Property(x => x.ActInvoiceSumWithVatAmount).HasPrecision(18, 2);
            e.Property(x => x.BasicCurrencyWithoutVatAmount).HasPrecision(18, 2);
            e.Property(x => x.BasicCurrencyWithVatAmount).HasPrecision(18, 2);
            e.Property(x => x.BalancePaidAmount).HasPrecision(18, 2);
            e.Property(x => x.BalanceTotalAmount).HasPrecision(18, 2);
            e.HasOne(x => x.OperationInvoice)
                .WithMany()
                .HasForeignKey(x => x.OperationInvoiceId)
                .OnDelete(DeleteBehavior.SetNull);
            e.HasIndex(x => x.ActNo);
        });
    }
}
