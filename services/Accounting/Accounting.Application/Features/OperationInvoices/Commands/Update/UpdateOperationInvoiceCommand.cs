using Accounting.Application.DTOs.OperationInvoice;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Update;

public record UpdateOperationInvoiceCommand(Guid Id, UpdateOperationInvoiceDto Dto) : IRequest<OperationInvoiceDto?>;
