using Accounting.Application.DTOs.OperationInvoice;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Queries.GetAllForList;

public sealed record GetAllOperationInvoicesForListQuery : IRequest<IReadOnlyList<OperationInvoiceListItemDto>>;
