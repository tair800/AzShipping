using MediatR;
using Request.Application.DTOs.RequestNegotiation;

namespace Request.Application.Features.RequestNegotiations.Queries.GetAll;

public sealed record GetAllRequestNegotiationsQuery(Guid? ClientId = null) : IRequest<IReadOnlyList<RequestNegotiationDto>>;
