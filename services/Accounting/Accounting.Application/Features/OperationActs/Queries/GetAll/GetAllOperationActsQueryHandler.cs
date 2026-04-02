using Accounting.Application.DTOs.OperationAct;
using Accounting.Domain.AggregatesModel.OperationActAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationActs.Queries.GetAll;

public sealed class GetAllOperationActsQueryHandler(IOperationActRepository repo)
    : IRequestHandler<GetAllOperationActsQuery, IReadOnlyList<OperationActListItemDto>>
{
    public async Task<IReadOnlyList<OperationActListItemDto>> Handle(GetAllOperationActsQuery request,
        CancellationToken cancellationToken)
    {
        var list = await repo.GetAllAsync(cancellationToken);
        return list.Select(OperationActMapper.ToListItemDto).ToList();
    }
}
