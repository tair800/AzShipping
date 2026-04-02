using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Delete;

public record DeleteOperationInvoiceCommand(Guid Id) : IRequest<bool>;
