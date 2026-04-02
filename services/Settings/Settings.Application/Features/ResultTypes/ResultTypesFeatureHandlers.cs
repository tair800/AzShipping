using MediatR;
using Settings.Application.DTOs.ResultType;
using Settings.Domain.AggregatesModel.ResultTypeAggregate;

namespace Settings.Application.Features.ResultTypes;

public sealed record GetAllResultTypesQuery : IRequest<IReadOnlyList<ResultTypeDto>>;
public sealed class GetAllResultTypesQueryHandler(IResultTypeRepository repository) : IRequestHandler<GetAllResultTypesQuery, IReadOnlyList<ResultTypeDto>>
{
    public async Task<IReadOnlyList<ResultTypeDto>> Handle(GetAllResultTypesQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new ResultTypeDto(e.Id, e.Name, e.Code, e.IsActive)).ToList();
    }
}
