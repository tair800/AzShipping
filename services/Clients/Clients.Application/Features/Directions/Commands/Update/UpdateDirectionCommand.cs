using Clients.Application.DTOs.Direction;
using MediatR;

namespace Clients.Application.Features.Directions.Commands.Update;

public sealed record UpdateDirectionCommand(Guid Id, UpdateDirectionDto Dto) : IRequest<DirectionDto?>;
