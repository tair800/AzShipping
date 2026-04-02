using Carrier.Application.DTOs.ShippingAgent;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;

namespace Carrier.Application.Features.ShippingAgents;

public static class ShippingAgentMapper
{
    public static ShippingAgentDto MapToDto(ShippingAgent? entity)
    {
        if (entity == null) return null!;
        return new ShippingAgentDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            CompanyName = entity.CompanyName,
            LocalName = entity.LocalName,
            Address1 = entity.Address1,
            Address2 = entity.Address2,
            CountryId = entity.CountryId,
            StateId = entity.StateId,
            CityId = entity.CityId,
            ZipCode = entity.ZipCode,
            VatNo = entity.VatNo,
            Email = entity.Email,
            EnglishName = entity.EnglishName,
            Position = entity.Position,
            BusinessPhone = entity.BusinessPhone,
            Mobile = entity.Mobile,
            Fax = entity.Fax,
            Phone = entity.Phone,
            Notes = entity.Notes,
            IsActive = entity.IsActive
        };
    }
}
