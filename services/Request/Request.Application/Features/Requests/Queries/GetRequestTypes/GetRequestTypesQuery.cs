using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Queries.GetRequestTypes;

public sealed record GetRequestTypesQuery(bool IncludeInactive = false) : IRequest<IReadOnlyList<RequestTypeDto>>;
