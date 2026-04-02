using MediatR;
using Request.Application.DTOs.Request;

namespace Request.Application.Features.Requests.Queries.GetAll;

public sealed record GetAllRequestsQuery(string? TypeCode = null, string? Mode = null, string? Direction = null, string? SubType = null) : IRequest<IReadOnlyList<RequestDto>>;
