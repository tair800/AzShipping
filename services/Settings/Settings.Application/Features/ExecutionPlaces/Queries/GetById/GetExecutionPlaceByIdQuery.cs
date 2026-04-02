using MediatR;
using Settings.Application.DTOs.ExecutionPlace;

namespace Settings.Application.Features.ExecutionPlaces.Queries.GetById;

public sealed record GetExecutionPlaceByIdQuery(Guid Id) : IRequest<ExecutionPlaceDto?>;
