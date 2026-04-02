namespace Accounting.Application.DTOs.OperationAct;

public class OperationActListItemDto
{
    public long Id { get; set; }
    public Guid? OperationInvoiceId { get; set; }
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
}

public class CreateOperationActDto
{
    public Guid? OperationInvoiceId { get; set; }
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
}
