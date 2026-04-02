using MediatR;
using Settings.Application.DTOs.RequestSource;

namespace Settings.Application.Features.RequestSources.Queries.GetAll;

public sealed record GetAllRequestSourcesQuery : IRequest<IReadOnlyList<RequestSourceDto>>;
