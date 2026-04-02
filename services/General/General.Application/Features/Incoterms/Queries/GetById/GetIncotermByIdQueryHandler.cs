using General.Application.DTOs.Incoterm;
using General.Application.Features.Incoterms;
using MediatR;
using General.Domain.AggregatesModel.IncotermAggregate;

namespace General.Application.Features.Incoterms.Queries.GetById;

public class GetIncotermByIdQueryHandler(IIncotermRepository repository)
    : IRequestHandler<GetIncotermByIdQuery, IncotermDto?>
{
    public async Task<IncotermDto?> Handle(GetIncotermByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return IncotermMapper.MapToDto(entity);
    }
}
