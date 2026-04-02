using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Update;

public sealed class UpdateVatDefinitionCommandHandler(IVatDefinitionRepository repository)
    : IRequestHandler<UpdateVatDefinitionCommand, VatDefinitionDto?>
{
    public async Task<VatDefinitionDto?> Handle(UpdateVatDefinitionCommand request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdTrackedAsync(request.Id, cancellationToken);
        if (e == null) return null;
        var d = request.Dto;
        var name = d.Name?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(name)) throw new InvalidOperationException("Name is required.");
        if (d.Percent < 0) throw new InvalidOperationException("Percent cannot be negative.");
        var buyCode = d.BuyingAccountCode?.Trim() ?? string.Empty;
        var sellCode = d.SellingAccountCode?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(buyCode)) throw new InvalidOperationException("Buying account code is required.");
        if (string.IsNullOrEmpty(sellCode)) throw new InvalidOperationException("Selling account code is required.");

        e.Name = name;
        e.Percent = d.Percent;
        e.IsAlcohol = d.IsAlcohol;
        e.BuyingAccountName = string.IsNullOrWhiteSpace(d.BuyingAccountName) ? null : d.BuyingAccountName.Trim();
        e.BuyingAccountCode = buyCode;
        e.SellingAccountName = string.IsNullOrWhiteSpace(d.SellingAccountName) ? null : d.SellingAccountName.Trim();
        e.SellingAccountCode = sellCode;
        e.Notes = string.IsNullOrWhiteSpace(d.Notes) ? null : d.Notes.Trim();
        e.IsActive = d.IsActive;
        e.UpdatedAtUtc = DateTime.UtcNow;
        await repository.UpdateAsync(e, cancellationToken);
        var loaded = await repository.GetByIdAsync(e.Id, cancellationToken);
        return loaded == null ? null : VatDefinitionMapper.ToDto(loaded);
    }
}
