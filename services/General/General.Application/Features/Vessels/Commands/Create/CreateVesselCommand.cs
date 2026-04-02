using General.Application.DTOs.Vessel;
using MediatR;

namespace General.Application.Features.Vessels.Commands.Create;

public record CreateVesselCommand(CreateVesselDto Dto) : IRequest<VesselDto>;
