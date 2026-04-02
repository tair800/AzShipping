using Clients.Application.DTOs.Direction;
using MediatR;

namespace Clients.Application.Features.Directions.Commands.Create;

public sealed record CreateDirectionCommand(CreateDirectionDto Dto) : IRequest<DirectionDto>;
