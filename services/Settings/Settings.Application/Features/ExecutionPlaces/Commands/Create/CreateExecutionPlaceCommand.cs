using MediatR;
using Settings.Application.DTOs.ExecutionPlace;

namespace Settings.Application.Features.ExecutionPlaces.Commands.Create;

public sealed record CreateExecutionPlaceCommand(CreateExecutionPlaceDto Dto) : IRequest<ExecutionPlaceDto>;
