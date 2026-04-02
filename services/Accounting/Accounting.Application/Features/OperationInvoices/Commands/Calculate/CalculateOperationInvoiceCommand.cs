using Accounting.Application.DTOs.OperationInvoice;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Calculate;

public record CalculateOperationInvoiceCommand(CalculateOperationInvoiceRequestDto Body) : IRequest<CalculateOperationInvoiceResponseDto>;
