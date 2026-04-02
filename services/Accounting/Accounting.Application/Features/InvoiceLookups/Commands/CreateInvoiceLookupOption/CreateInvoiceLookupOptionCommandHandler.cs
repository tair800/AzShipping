using Accounting.Application.DTOs.InvoiceLookup;
using Accounting.Application.InvoiceLookups;
using Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;
using MediatR;

namespace Accounting.Application.Features.InvoiceLookups.Commands.CreateInvoiceLookupOption;

public class CreateInvoiceLookupOptionCommandHandler(IInvoiceLookupRepository repository)
    : IRequestHandler<CreateInvoiceLookupOptionCommand, CreateInvoiceLookupOutcome>
{
    public async Task<CreateInvoiceLookupOutcome> Handle(CreateInvoiceLookupOptionCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (!InvoiceLookupCategoryKeys.TryParseApiKey(dto.Category, out var category))
            return new CreateInvoiceLookupOutcome(false, "Invalid category.", null);

        if (!InvoiceLookupCategoryKeys.IsUserCreatable(category))
            return new CreateInvoiceLookupOutcome(false,
                "Only expense center and special code can be added here. Use Settings for warehouses (execution places), departments, etc.", null);

        var code = (dto.Code ?? "").Trim();
        var name = (dto.Name ?? "").Trim();
        if (code.Length == 0 || name.Length == 0)
            return new CreateInvoiceLookupOutcome(false, "Code and name are required.", null);
        if (code.Length > 80 || name.Length > 300)
            return new CreateInvoiceLookupOutcome(false, "Code or name is too long.", null);

        if (await repository.ExistsCodeAsync(category, code, cancellationToken))
            return new CreateInvoiceLookupOutcome(false, "This code already exists for that list.", null);

        var lastSort = 0;
        var existing = await repository.GetActiveAsync(category, cancellationToken);
        if (existing.Count > 0)
            lastSort = existing.Max(x => x.SortOrder);

        var entity = new InvoiceLookupOption
        {
            Id = Guid.NewGuid(),
            Category = category,
            Code = code,
            Name = name,
            SortOrder = lastSort + 1,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        return new CreateInvoiceLookupOutcome(true, null,
            new InvoiceLookupOptionDto(
                InvoiceLookupCategoryKeys.ToApiKey(category),
                entity.Code,
                entity.Name,
                entity.SortOrder));
    }
}
