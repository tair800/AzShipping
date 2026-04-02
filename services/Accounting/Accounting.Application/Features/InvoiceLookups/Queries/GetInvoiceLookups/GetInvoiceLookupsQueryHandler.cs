using Accounting.Application.DTOs.InvoiceLookup;
using Accounting.Application.InvoiceLookups;
using Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;
using MediatR;

namespace Accounting.Application.Features.InvoiceLookups.Queries.GetInvoiceLookups;

public class GetInvoiceLookupsQueryHandler(IInvoiceLookupRepository repository)
    : IRequestHandler<GetInvoiceLookupsQuery, IReadOnlyList<InvoiceLookupOptionDto>>
{
    public async Task<IReadOnlyList<InvoiceLookupOptionDto>> Handle(GetInvoiceLookupsQuery request,
        CancellationToken cancellationToken)
    {
        InvoiceLookupCategory? category = null;
        if (!string.IsNullOrWhiteSpace(request.Category))
        {
            if (!InvoiceLookupCategoryKeys.TryParseApiKey(request.Category, out var parsed))
                throw new ArgumentException("Invalid category.", nameof(request.Category));
            category = parsed;
            if (InvoiceLookupCategoryKeys.IsMergedFromSettings(parsed))
                return [];
        }

        var rows = await repository.GetActiveAsync(category, cancellationToken);
        return rows
            .Where(r => !InvoiceLookupCategoryKeys.IsMergedFromSettings(r.Category))
            .Select(r => new InvoiceLookupOptionDto(
                InvoiceLookupCategoryKeys.ToApiKey(r.Category),
                r.Code,
                r.Name,
                r.SortOrder)).ToList();
    }
}
