using Accounting.Application.DTOs.OperationAct;
using Accounting.Domain.AggregatesModel.OperationActAggregate;

namespace Accounting.Application.Features.OperationActs;

public static class OperationActMapper
{
    public static OperationActListItemDto ToListItemDto(OperationAct a) => new()
    {
        Id = a.Id,
        OperationInvoiceId = a.OperationInvoiceId,
        Payer = a.Payer,
        OrderNo = a.OrderNo,
        OrderDate = a.OrderDate,
        ActNo = a.ActNo,
        ActDischargeDate = a.ActDischargeDate,
        ActSumWithoutVatAmount = a.ActSumWithoutVatAmount,
        ActSumWithoutVatCurrency = a.ActSumWithoutVatCurrency,
        ActSumWithVatAmount = a.ActSumWithVatAmount,
        ActSumWithVatCurrency = a.ActSumWithVatCurrency,
        InvoiceNo = a.InvoiceNo,
        ActInvoiceDate = a.ActInvoiceDate,
        ActInvoiceSumWithoutVatAmount = a.ActInvoiceSumWithoutVatAmount,
        ActInvoiceSumWithoutVatCurrency = a.ActInvoiceSumWithoutVatCurrency,
        ActInvoiceSumWithVatAmount = a.ActInvoiceSumWithVatAmount,
        ActInvoiceSumWithVatCurrency = a.ActInvoiceSumWithVatCurrency,
        BasicCurrencyWithoutVatAmount = a.BasicCurrencyWithoutVatAmount,
        BasicCurrencyWithoutVatCurrency = a.BasicCurrencyWithoutVatCurrency,
        BasicCurrencyWithVatAmount = a.BasicCurrencyWithVatAmount,
        BasicCurrencyWithVatCurrency = a.BasicCurrencyWithVatCurrency,
        BalancePaidAmount = a.BalancePaidAmount,
        BalanceTotalAmount = a.BalanceTotalAmount,
        BalanceCurrency = a.BalanceCurrency,
    };

    public static OperationAct FromCreateDto(CreateOperationActDto dto, int sortOrder) => new()
    {
        OperationInvoiceId = dto.OperationInvoiceId,
        Payer = dto.Payer?.Trim() ?? "",
        OrderNo = dto.OrderNo?.Trim() ?? "",
        OrderDate = dto.OrderDate,
        ActNo = dto.ActNo?.Trim() ?? "",
        ActDischargeDate = dto.ActDischargeDate,
        ActSumWithoutVatAmount = dto.ActSumWithoutVatAmount,
        ActSumWithoutVatCurrency = dto.ActSumWithoutVatCurrency,
        ActSumWithVatAmount = dto.ActSumWithVatAmount,
        ActSumWithVatCurrency = dto.ActSumWithVatCurrency,
        InvoiceNo = dto.InvoiceNo?.Trim() ?? "",
        ActInvoiceDate = dto.ActInvoiceDate,
        ActInvoiceSumWithoutVatAmount = dto.ActInvoiceSumWithoutVatAmount,
        ActInvoiceSumWithoutVatCurrency = dto.ActInvoiceSumWithoutVatCurrency,
        ActInvoiceSumWithVatAmount = dto.ActInvoiceSumWithVatAmount,
        ActInvoiceSumWithVatCurrency = dto.ActInvoiceSumWithVatCurrency,
        BasicCurrencyWithoutVatAmount = dto.BasicCurrencyWithoutVatAmount,
        BasicCurrencyWithoutVatCurrency = dto.BasicCurrencyWithoutVatCurrency,
        BasicCurrencyWithVatAmount = dto.BasicCurrencyWithVatAmount,
        BasicCurrencyWithVatCurrency = dto.BasicCurrencyWithVatCurrency,
        BalancePaidAmount = dto.BalancePaidAmount,
        BalanceTotalAmount = dto.BalanceTotalAmount,
        BalanceCurrency = dto.BalanceCurrency,
        SortOrder = sortOrder,
    };
}
