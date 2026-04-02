using General.Application.DTOs.Vas;
using MediatR;

namespace General.Application.Features.Vas.Commands.Update;

public record UpdateVasCommand(Guid Id, UpdateVasDto Dto) : IRequest<VasDto?>;
