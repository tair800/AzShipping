using MediatR;
using Settings.Application.DTOs.ExecutionPlace;

namespace Settings.Application.Features.ExecutionPlaces.Queries.GetAll;

public sealed record GetAllExecutionPlacesQuery : IRequest<IReadOnlyList<ExecutionPlaceDto>>;
