using MediatR;
using Request.Application.DTOs.RequestNegotiation;

namespace Request.Application.Features.RequestNegotiations.Queries.GetById;

public sealed record GetRequestNegotiationByIdQuery(Guid Id) : IRequest<RequestNegotiationDto?>;
