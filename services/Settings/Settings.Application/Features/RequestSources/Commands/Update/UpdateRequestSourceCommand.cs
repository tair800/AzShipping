using MediatR;
using Settings.Application.DTOs.RequestSource;

namespace Settings.Application.Features.RequestSources.Commands.Update;

public sealed record UpdateRequestSourceCommand(Guid Id, UpdateRequestSourceDto Dto) : IRequest<RequestSourceDto?>;
