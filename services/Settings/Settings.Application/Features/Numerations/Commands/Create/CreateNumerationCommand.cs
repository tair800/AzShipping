using MediatR;
using Settings.Application.DTOs.Numeration;

namespace Settings.Application.Features.Numerations.Commands.Create;

public sealed record CreateNumerationCommand(CreateNumerationDto Dto) : IRequest<NumerationDto>;
