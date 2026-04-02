using General.Application.DTOs.Incoterm;
using General.Application.Features.Incoterms;
using MediatR;
using General.Domain.AggregatesModel.IncotermAggregate;

namespace General.Application.Features.Incoterms.Queries.GetAll;

public class GetAllIncotermsQueryHandler(IIncotermRepository repository)
    : IRequestHandler<GetAllIncotermsQuery, IReadOnlyList<IncotermDto>>
{
    public async Task<IReadOnlyList<IncotermDto>> Handle(GetAllIncotermsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(request.IsActive, request.IsDeleted, cancellationToken);
        return items.Select(IncotermMapper.MapToDto).ToList();
    }
}
