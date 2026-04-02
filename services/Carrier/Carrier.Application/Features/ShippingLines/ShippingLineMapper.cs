using Carrier.Application.DTOs.ShippingLine;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;

namespace Carrier.Application.Features.ShippingLines;

public static class ShippingLineMapper
{
    public static ShippingLineDto MapToDto(ShippingLine? entity)
    {
        if (entity == null) return null!;
        return new ShippingLineDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Code = entity.Code,
            ScacCode = entity.ScacCode,
            Cbsa = entity.Cbsa,
            Caat = entity.Caat,
            Name = entity.Name,
            LocalName = entity.LocalName,
            ShippingAgent = entity.ShippingAgent,
            ShippingAgentCompanyId = entity.ShippingAgentCompanyId,
            Website = entity.Website,
            VatNo = entity.VatNo,
            Notes = entity.Notes,
            IsActive = entity.IsActive
        };
    }
}
