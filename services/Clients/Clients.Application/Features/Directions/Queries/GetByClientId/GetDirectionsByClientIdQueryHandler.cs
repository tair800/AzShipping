using Clients.Application.DTOs.Direction;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using MediatR;

namespace Clients.Application.Features.Directions.Queries.GetByClientId;

public sealed class GetDirectionsByClientIdQueryHandler(IDirectionRepository repository) : IRequestHandler<GetDirectionsByClientIdQuery, IReadOnlyList<DirectionDto>>
{
    public async Task<IReadOnlyList<DirectionDto>> Handle(GetDirectionsByClientIdQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByClientIdAsync(request.ClientId, cancellationToken);
        return list.Select(e => new DirectionDto
        {
            Id = e.Id,
            ClientId = e.ClientId,
            FromCountryId = e.FromCountryId,
            FromCityId = e.FromCityId,
            ToCountryId = e.ToCountryId,
            ToCityId = e.ToCityId,
            Note = e.Note,
            Comments = e.Comments
        }).ToList();
    }
}
