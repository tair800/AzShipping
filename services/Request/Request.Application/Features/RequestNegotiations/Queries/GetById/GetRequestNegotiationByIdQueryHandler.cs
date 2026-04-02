using MediatR;
using Request.Application.DTOs.RequestNegotiation;
using Request.Application.Features.RequestNegotiations;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;

namespace Request.Application.Features.RequestNegotiations.Queries.GetById;

public sealed class GetRequestNegotiationByIdQueryHandler(IRequestNegotiationRepository repository) : IRequestHandler<GetRequestNegotiationByIdQuery, RequestNegotiationDto?>
{
    public async Task<RequestNegotiationDto?> Handle(GetRequestNegotiationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : RequestNegotiationMapper.MapToDto(entity);
    }
}
