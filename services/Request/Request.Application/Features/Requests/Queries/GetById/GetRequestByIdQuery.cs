using MediatR;
using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Queries.GetById;

public sealed record GetRequestByIdQuery(Guid Id) : IRequest<RequestDto?>;
