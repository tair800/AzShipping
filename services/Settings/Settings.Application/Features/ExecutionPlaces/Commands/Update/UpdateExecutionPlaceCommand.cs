using MediatR;
using Settings.Application.DTOs.ExecutionPlace;

namespace Settings.Application.Features.ExecutionPlaces.Commands.Update;

public sealed record UpdateExecutionPlaceCommand(Guid Id, UpdateExecutionPlaceDto Dto) : IRequest<ExecutionPlaceDto?>;
