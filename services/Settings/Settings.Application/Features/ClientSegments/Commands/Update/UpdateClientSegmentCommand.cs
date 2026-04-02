using MediatR;
using Settings.Application.DTOs.ClientSegment;

namespace Settings.Application.Features.ClientSegments.Commands.Update;

public sealed record UpdateClientSegmentCommand(Guid Id, UpdateClientSegmentDto Dto) : IRequest<ClientSegmentDto?>;
