using Request.Application.DTOs.RequestNegotiation;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;

namespace Request.Application.Features.RequestNegotiations;

public static class RequestNegotiationMapper
{
    public static RequestNegotiationDto MapToDto(RequestNegotiation? entity)
    {
        if (entity == null) return null!;
        return new RequestNegotiationDto(
            entity.Id,
            entity.ClientId,
            entity.ClientName,
            entity.WayOfNegotiationId,
            entity.WayOfNegotiationName,
            entity.CreationDate,
            entity.Question,
            entity.Answer,
            entity.Result);
    }
}
