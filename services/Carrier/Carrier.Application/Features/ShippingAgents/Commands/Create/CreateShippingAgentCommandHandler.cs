using Carrier.Application.DTOs.ShippingAgent;
using Carrier.Application.Features.ShippingAgents;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Commands.Create;

public class CreateShippingAgentCommandHandler(IShippingAgentRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateShippingAgentCommand, ShippingAgentDto>
{
    public async Task<ShippingAgentDto> Handle(CreateShippingAgentCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new ShippingAgent
        {
            Id = Guid.NewGuid(),
            CompanyName = dto.CompanyName,
            LocalName = dto.LocalName,
            Address1 = dto.Address1,
            Address2 = dto.Address2,
            CountryId = dto.CountryId,
            StateId = dto.StateId,
            CityId = dto.CityId,
            ZipCode = dto.ZipCode,
            VatNo = dto.VatNo,
            Email = dto.Email,
            EnglishName = dto.EnglishName,
            Position = dto.Position,
            BusinessPhone = dto.BusinessPhone,
            Mobile = dto.Mobile,
            Fax = dto.Fax,
            Phone = dto.Phone,
            Notes = dto.Notes,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Shipping agent created", $"shipping agent: {entity.CompanyName} • id: {entity.Id}", null, null, cancellationToken);
        return ShippingAgentMapper.MapToDto(created!);
    }
}
