using MediatR;

namespace Settings.Application.Features.ExecutionPlaces.Commands.Delete;

public sealed record DeleteExecutionPlaceCommand(Guid Id) : IRequest<bool>;
