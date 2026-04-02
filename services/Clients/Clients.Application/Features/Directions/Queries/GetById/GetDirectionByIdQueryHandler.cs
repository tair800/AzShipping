using Clients.Application.DTOs.Direction;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using MediatR;

namespace Clients.Application.Features.Directions.Queries.GetById;

public sealed class GetDirectionByIdQueryHandler(IDirectionRepository repository) : IRequestHandler<GetDirectionByIdQuery, DirectionDto?>
{
    public async Task<DirectionDto?> Handle(GetDirectionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : new DirectionDto
        {
            Id = entity.Id,
            ClientId = entity.ClientId,
            FromCountryId = entity.FromCountryId,
            FromCityId = entity.FromCityId,
            ToCountryId = entity.ToCountryId,
            ToCityId = entity.ToCityId,
            Note = entity.Note,
            Comments = entity.Comments
        };
    }
}
