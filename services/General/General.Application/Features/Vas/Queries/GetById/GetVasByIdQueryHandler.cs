using General.Application.DTOs.Vas;
using General.Application.Features.Vas;
using General.Domain.AggregatesModel.VasAggregate;
using MediatR;

namespace General.Application.Features.Vas.Queries.GetById;

public class GetVasByIdQueryHandler(IVasRepository repository)
    : IRequestHandler<GetVasByIdQuery, VasDto?>
{
    public async Task<VasDto?> Handle(GetVasByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return VasMapper.MapToDto(entity);
    }
}
