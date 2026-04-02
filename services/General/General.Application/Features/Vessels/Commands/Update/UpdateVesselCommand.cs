using General.Application.DTOs.Vessel;
using MediatR;

namespace General.Application.Features.Vessels.Commands.Update;

public record UpdateVesselCommand(Guid Id, UpdateVesselDto Dto) : IRequest<VesselDto?>;
