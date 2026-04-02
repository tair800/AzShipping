using MediatR;
using Settings.Application.DTOs.ClientSegment;

namespace Settings.Application.Features.ClientSegments.Commands.Create;

public sealed record CreateClientSegmentCommand(CreateClientSegmentDto Dto) : IRequest<ClientSegmentDto>;
