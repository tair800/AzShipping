using Accounting.Application.DTOs.InvoiceLookup;
using MediatR;

namespace Accounting.Application.Features.InvoiceLookups.Commands.CreateInvoiceLookupOption;

public record CreateInvoiceLookupOptionCommand(CreateInvoiceLookupOptionDto Dto) : IRequest<CreateInvoiceLookupOutcome>;
