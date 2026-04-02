using General.Application.DTOs.Vessel;
using MediatR;

namespace General.Application.Features.Vessels.Queries.GetAll;

public record GetAllVesselsQuery(bool? IsActive, bool? IsDeleted) : IRequest<IReadOnlyList<VesselDto>>;
