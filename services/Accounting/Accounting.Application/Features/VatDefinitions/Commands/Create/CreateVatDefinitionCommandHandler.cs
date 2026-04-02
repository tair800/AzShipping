using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Create;

public sealed class CreateVatDefinitionCommandHandler(IVatDefinitionRepository repository)
    : IRequestHandler<CreateVatDefinitionCommand, VatDefinitionDto>
{
    public async Task<VatDefinitionDto> Handle(CreateVatDefinitionCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var name = d.Name?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(name)) throw new InvalidOperationException("Name is required.");
        if (d.Percent < 0) throw new InvalidOperationException("Percent cannot be negative.");
        var buyCode = d.BuyingAccountCode?.Trim() ?? string.Empty;
        var sellCode = d.SellingAccountCode?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(buyCode)) throw new InvalidOperationException("Buying account code is required.");
        if (string.IsNullOrEmpty(sellCode)) throw new InvalidOperationException("Selling account code is required.");

        var now = DateTime.UtcNow;
        var entity = new VatDefinition
        {
            Id = Guid.NewGuid(),
            CreatedAtUtc = now,
            Name = name,
            Percent = d.Percent,
            IsAlcohol = d.IsAlcohol,
            BuyingAccountName = string.IsNullOrWhiteSpace(d.BuyingAccountName) ? null : d.BuyingAccountName.Trim(),
            BuyingAccountCode = buyCode,
            SellingAccountName = string.IsNullOrWhiteSpace(d.SellingAccountName) ? null : d.SellingAccountName.Trim(),
            SellingAccountCode = sellCode,
            Notes = string.IsNullOrWhiteSpace(d.Notes) ? null : d.Notes.Trim(),
            IsActive = d.IsActive,
        };
        await repository.AddAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return VatDefinitionMapper.ToDto(loaded!);
    }
}
