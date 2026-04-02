using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Queries.GetRequestTypeById;

public sealed record GetRequestTypeByIdQuery(Guid Id) : IRequest<RequestTypeDto?>;
