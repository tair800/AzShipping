using MediatR;
using Settings.Application.DTOs.ExecutionPlace;
using Settings.Application.Features.ExecutionPlaces;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

namespace Settings.Application.Features.ExecutionPlaces.Queries.GetAll;

public sealed class GetAllExecutionPlacesQueryHandler(IExecutionPlaceRepository repository) : IRequestHandler<GetAllExecutionPlacesQuery, IReadOnlyList<ExecutionPlaceDto>>
{
    public async Task<IReadOnlyList<ExecutionPlaceDto>> Handle(GetAllExecutionPlacesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(ExecutionPlaceMapper.MapToDto).ToList();
    }
}
