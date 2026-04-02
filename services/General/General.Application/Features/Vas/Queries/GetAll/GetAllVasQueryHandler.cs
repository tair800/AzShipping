using General.Application.DTOs.Vas;
using General.Application.Features.Vas;
using General.Domain.AggregatesModel.VasAggregate;
using MediatR;

namespace General.Application.Features.Vas.Queries.GetAll;

public class GetAllVasQueryHandler(IVasRepository repository)
    : IRequestHandler<GetAllVasQuery, IReadOnlyList<VasDto>>
{
    public async Task<IReadOnlyList<VasDto>> Handle(GetAllVasQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(request.IsActive, request.IsDeleted, cancellationToken);
        return items.Select(VasMapper.MapToDto).ToList();
    }
}
