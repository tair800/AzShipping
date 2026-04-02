using MediatR;
using Settings.Application.DTOs.RequestSource;

namespace Settings.Application.Features.RequestSources.Queries.GetById;

public sealed record GetRequestSourceByIdQuery(Guid Id) : IRequest<RequestSourceDto?>;
