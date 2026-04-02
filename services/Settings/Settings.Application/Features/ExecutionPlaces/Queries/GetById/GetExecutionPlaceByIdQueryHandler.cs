using MediatR;
using Settings.Application.DTOs.ExecutionPlace;
using Settings.Application.Features.ExecutionPlaces;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

namespace Settings.Application.Features.ExecutionPlaces.Queries.GetById;

public sealed class GetExecutionPlaceByIdQueryHandler(IExecutionPlaceRepository repository) : IRequestHandler<GetExecutionPlaceByIdQuery, ExecutionPlaceDto?>
{
    public async Task<ExecutionPlaceDto?> Handle(GetExecutionPlaceByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : ExecutionPlaceMapper.MapToDto(entity);
    }
}
