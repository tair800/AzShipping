using MediatR;
using Settings.Application.DTOs.Uom;
using Settings.Application.Features.Uoms;
using Settings.Domain.AggregatesModel.UomAggregate;

namespace Settings.Application.Features.Uoms.Queries.GetById;

public sealed class GetUomByIdQueryHandler(IUomRepository repository) : IRequestHandler<GetUomByIdQuery, UomDto?>
{
    public async Task<UomDto?> Handle(GetUomByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : UomMapper.MapToDto(entity);
    }
}
