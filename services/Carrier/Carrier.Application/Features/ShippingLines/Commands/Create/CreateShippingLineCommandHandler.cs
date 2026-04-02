using Carrier.Application.DTOs.ShippingLine;
using Carrier.Application.Features.ShippingLines;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Commands.Create;

public class CreateShippingLineCommandHandler(IShippingLineRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateShippingLineCommand, ShippingLineDto>
{
    public async Task<ShippingLineDto> Handle(CreateShippingLineCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new ShippingLine
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            ScacCode = dto.ScacCode,
            Cbsa = dto.Cbsa,
            Caat = dto.Caat,
            Name = dto.Name,
            LocalName = dto.LocalName,
            ShippingAgent = dto.ShippingAgent,
            ShippingAgentCompanyId = dto.ShippingAgentCompanyId,
            Website = dto.Website,
            VatNo = dto.VatNo,
            Notes = dto.Notes,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Shipping line created", $"shipping line: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return ShippingLineMapper.MapToDto(created!);
    }
}
