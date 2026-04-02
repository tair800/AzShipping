using Clients.Application.DTOs.Direction;
using MediatR;

namespace Clients.Application.Features.Directions.Queries.GetById;

public sealed record GetDirectionByIdQuery(Guid Id) : IRequest<DirectionDto?>;
