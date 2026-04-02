using Accounting.Application.DTOs.InvoiceLookup;
using MediatR;

namespace Accounting.Application.Features.InvoiceLookups.Queries.GetInvoiceLookups;

public record GetInvoiceLookupsQuery(string? Category) : IRequest<IReadOnlyList<InvoiceLookupOptionDto>>;
