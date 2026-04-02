using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

namespace Accounting.Domain.AggregatesModel.OperationActAggregate;

public class OperationAct
{
    public long Id { get; set; }

    /// <summary>Optional link to an operation invoice (AZ-INV-*) when the act is billed against that invoice.</summary>
    public Guid? OperationInvoiceId { get; set; }
    public OperationInvoice? OperationInvoice { get; set; }

    public string Payer { get; set; } = "";
    public string OrderNo { get; set; } = "";
    public DateOnly? OrderDate { get; set; }
    public string ActNo { get; set; } = "";
    public DateOnly? ActDischargeDate { get; set; }

    public decimal? ActSumWithoutVatAmount { get; set; }
    public string? ActSumWithoutVatCurrency { get; set; }
    public decimal? ActSumWithVatAmount { get; set; }
    public string? ActSumWithVatCurrency { get; set; }

    public string InvoiceNo { get; set; } = "";
    public DateOnly? ActInvoiceDate { get; set; }
    public decimal? ActInvoiceSumWithoutVatAmount { get; set; }
    public string? ActInvoiceSumWithoutVatCurrency { get; set; }
    public decimal? ActInvoiceSumWithVatAmount { get; set; }
    public string? ActInvoiceSumWithVatCurrency { get; set; }

    public decimal? BasicCurrencyWithoutVatAmount { get; set; }
    public string? BasicCurrencyWithoutVatCurrency { get; set; }
    public decimal? BasicCurrencyWithVatAmount { get; set; }
    public string? BasicCurrencyWithVatCurrency { get; set; }

    public decimal? BalancePaidAmount { get; set; }
    public decimal? BalanceTotalAmount { get; set; }
    public string? BalanceCurrency { get; set; }

    public int SortOrder { get; set; }
}
