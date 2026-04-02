using MediatR;
using Request.Application.DTOs.RequestNegotiation;

namespace Request.Application.Features.RequestNegotiations.Commands.Update;

public sealed record UpdateRequestNegotiationCommand(Guid Id, UpdateRequestNegotiationDto Dto) : IRequest<RequestNegotiationDto?>;
