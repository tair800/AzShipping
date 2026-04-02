using MediatR;

namespace Settings.Application.Features.ClientSegments.Commands.Delete;

public sealed record DeleteClientSegmentCommand(Guid Id) : IRequest<bool>;
