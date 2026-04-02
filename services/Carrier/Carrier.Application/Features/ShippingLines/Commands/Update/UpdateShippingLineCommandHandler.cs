using Carrier.Application.DTOs.ShippingLine;
using Carrier.Application.Features.ShippingLines;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Commands.Update;

public class UpdateShippingLineCommandHandler(IShippingLineRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateShippingLineCommand, ShippingLineDto?>
{
    public async Task<ShippingLineDto?> Handle(UpdateShippingLineCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.Code = dto.Code;
        existing.ScacCode = dto.ScacCode;
        existing.Cbsa = dto.Cbsa;
        existing.Caat = dto.Caat;
        existing.Name = dto.Name;
        existing.LocalName = dto.LocalName;
        existing.ShippingAgent = dto.ShippingAgent;
        existing.ShippingAgentCompanyId = dto.ShippingAgentCompanyId;
        existing.Website = dto.Website;
        existing.VatNo = dto.VatNo;
        existing.Notes = dto.Notes;
        existing.IsActive = dto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Shipping line updated", $"shipping line: {existing.Name} • id: {existing.Id}", null, null, cancellationToken);
        return ShippingLineMapper.MapToDto(updated!);
    }
}
