using General.Application.DTOs.Vessel;
using General.Application.Features.Vessels;
using General.Domain.AggregatesModel.VesselAggregate;
using MediatR;

namespace General.Application.Features.Vessels.Queries.GetById;

public class GetVesselByIdQueryHandler(IVesselRepository repository)
    : IRequestHandler<GetVesselByIdQuery, VesselDto?>
{
    public async Task<VesselDto?> Handle(GetVesselByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return VesselMapper.MapToDto(entity);
    }
}
