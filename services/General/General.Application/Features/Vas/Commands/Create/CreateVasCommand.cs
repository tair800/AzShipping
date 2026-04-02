using General.Application.DTOs.Vas;
using MediatR;

namespace General.Application.Features.Vas.Commands.Create;

public record CreateVasCommand(CreateVasDto Dto) : IRequest<VasDto>;
