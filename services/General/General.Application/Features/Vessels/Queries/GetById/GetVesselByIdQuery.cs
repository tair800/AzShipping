using General.Application.DTOs.Vessel;
using MediatR;

namespace General.Application.Features.Vessels.Queries.GetById;

public record GetVesselByIdQuery(Guid Id) : IRequest<VesselDto?>;
