using MediatR;
using Settings.Application.DTOs.Numeration;

namespace Settings.Application.Features.Numerations.Commands.Update;

public sealed record UpdateNumerationCommand(Guid Id, UpdateNumerationDto Dto) : IRequest<NumerationDto?>;
