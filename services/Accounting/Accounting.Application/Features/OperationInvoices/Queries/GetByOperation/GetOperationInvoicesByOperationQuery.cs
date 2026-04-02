using Accounting.Application.DTOs.OperationInvoice;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Queries.GetByOperation;

public record GetOperationInvoicesByOperationQuery(Guid OperationId) : IRequest<IReadOnlyList<OperationInvoiceDto>>;
