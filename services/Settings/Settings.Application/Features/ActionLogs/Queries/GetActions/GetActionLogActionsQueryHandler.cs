using MediatR;
using Settings.Domain.AggregatesModel.ActionLogAggregate;

namespace Settings.Application.Features.ActionLogs.Queries.GetActions;

public sealed class GetActionLogActionsQueryHandler(IActionLogRepository repository)
    : IRequestHandler<GetActionLogActionsQuery, IReadOnlyList<string>>
{
    public async Task<IReadOnlyList<string>> Handle(GetActionLogActionsQuery request, CancellationToken ct)
        => await repository.GetDistinctActionsAsync(ct);
}
