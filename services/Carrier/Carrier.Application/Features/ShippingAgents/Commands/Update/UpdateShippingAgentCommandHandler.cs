using Carrier.Application.DTOs.ShippingAgent;
using Carrier.Application.Features.ShippingAgents;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Commands.Update;

public class UpdateShippingAgentCommandHandler(IShippingAgentRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateShippingAgentCommand, ShippingAgentDto?>
{
    public async Task<ShippingAgentDto?> Handle(UpdateShippingAgentCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.CompanyName = dto.CompanyName;
        existing.LocalName = dto.LocalName;
        existing.Address1 = dto.Address1;
        existing.Address2 = dto.Address2;
        existing.CountryId = dto.CountryId;
        existing.StateId = dto.StateId;
        existing.CityId = dto.CityId;
        existing.ZipCode = dto.ZipCode;
        existing.VatNo = dto.VatNo;
        existing.Email = dto.Email;
        existing.EnglishName = dto.EnglishName;
        existing.Position = dto.Position;
        existing.BusinessPhone = dto.BusinessPhone;
        existing.Mobile = dto.Mobile;
        existing.Fax = dto.Fax;
        existing.Phone = dto.Phone;
        existing.Notes = dto.Notes;
        existing.IsActive = dto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Shipping agent updated", $"shipping agent: {existing.CompanyName} • id: {existing.Id}", null, null, cancellationToken);
        return ShippingAgentMapper.MapToDto(updated!);
    }
}
