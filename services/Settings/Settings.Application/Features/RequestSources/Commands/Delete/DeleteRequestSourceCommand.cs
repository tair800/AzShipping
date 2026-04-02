using MediatR;

namespace Settings.Application.Features.RequestSources.Commands.Delete;

public sealed record DeleteRequestSourceCommand(Guid Id) : IRequest<bool>;
