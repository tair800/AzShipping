using MediatR;
using Settings.Application.DTOs.Uom;
using Settings.Application.Features.Uoms;
using Settings.Domain.AggregatesModel.UomAggregate;

namespace Settings.Application.Features.Uoms.Queries.GetAll;

public sealed class GetAllUomsQueryHandler(IUomRepository repository) : IRequestHandler<GetAllUomsQuery, IReadOnlyList<UomDto>>
{
    public async Task<IReadOnlyList<UomDto>> Handle(GetAllUomsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(UomMapper.MapToDto).ToList();
    }
}
