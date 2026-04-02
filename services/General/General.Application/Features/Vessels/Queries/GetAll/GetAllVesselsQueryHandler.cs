using General.Application.DTOs.Vessel;
using General.Application.Features.Vessels;
using General.Domain.AggregatesModel.VesselAggregate;
using MediatR;

namespace General.Application.Features.Vessels.Queries.GetAll;

public class GetAllVesselsQueryHandler(IVesselRepository repository)
    : IRequestHandler<GetAllVesselsQuery, IReadOnlyList<VesselDto>>
{
    public async Task<IReadOnlyList<VesselDto>> Handle(GetAllVesselsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(request.IsActive, request.IsDeleted, cancellationToken);
        return items.Select(VesselMapper.MapToDto).ToList();
    }
}
