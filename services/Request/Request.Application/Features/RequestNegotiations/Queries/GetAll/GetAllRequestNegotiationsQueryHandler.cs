using MediatR;
using Request.Application.DTOs.RequestNegotiation;
using Request.Application.Features.RequestNegotiations;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;

namespace Request.Application.Features.RequestNegotiations.Queries.GetAll;

public sealed class GetAllRequestNegotiationsQueryHandler(IRequestNegotiationRepository repository) : IRequestHandler<GetAllRequestNegotiationsQuery, IReadOnlyList<RequestNegotiationDto>>
{
    public async Task<IReadOnlyList<RequestNegotiationDto>> Handle(GetAllRequestNegotiationsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(request.ClientId, cancellationToken);
        return list.Select(RequestNegotiationMapper.MapToDto).ToList();
    }
}
