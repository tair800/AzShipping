using MediatR;
using Request.Application.DTOs.RequestNegotiation;

namespace Request.Application.Features.RequestNegotiations.Commands.Create;

public sealed record CreateRequestNegotiationCommand(CreateRequestNegotiationDto Dto) : IRequest<RequestNegotiationDto>;
