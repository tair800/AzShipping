using Accounting.Application.DTOs.OperationInvoice;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Queries.GetById;

public record GetOperationInvoiceByIdQuery(Guid Id) : IRequest<OperationInvoiceDto?>;
