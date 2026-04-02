using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Commands.Delete;

public class DeleteOperationInvoiceCommandHandler(IOperationInvoiceRepository invoiceRepo)
    : IRequestHandler<DeleteOperationInvoiceCommand, bool>
{
    public async Task<bool> Handle(DeleteOperationInvoiceCommand request, CancellationToken cancellationToken)
    {
        var existing = await invoiceRepo.GetByIdWithLinesAsync(request.Id, cancellationToken);
        if (existing == null)
            return false;
        await invoiceRepo.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
