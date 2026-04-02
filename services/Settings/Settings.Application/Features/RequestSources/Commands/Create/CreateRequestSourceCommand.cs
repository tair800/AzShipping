using MediatR;
using Settings.Application.DTOs.RequestSource;

namespace Settings.Application.Features.RequestSources.Commands.Create;

public sealed record CreateRequestSourceCommand(CreateRequestSourceDto Dto) : IRequest<RequestSourceDto>;
