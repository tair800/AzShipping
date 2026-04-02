using MediatR;
using Settings.Application.DTOs.Numeration;

namespace Settings.Application.Features.Numerations.Commands.Generate;

public sealed record GenerateNumerationCommand(NumerationGenerateRequestDto Dto) : IRequest<NumerationGenerateResponseDto>;
