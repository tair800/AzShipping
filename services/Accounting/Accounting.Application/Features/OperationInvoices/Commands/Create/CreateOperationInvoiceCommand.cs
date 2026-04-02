using Accounting.Application.DTOs.OperationInvoice;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Create;

public record CreateOperationInvoiceCommand(CreateOperationInvoiceDto Dto) : IRequest<OperationInvoiceDto>;
