using MediatR;
using Settings.Application.DTOs.SystemLog;
using Settings.Domain.AggregatesModel.SystemLogAggregate;

namespace Settings.Application.Features.SystemLogs.Queries.GetPaged;

public sealed class GetSystemLogsPagedQueryHandler(ISystemLogRepository repository)
    : IRequestHandler<GetSystemLogsPagedQuery, GetSystemLogsPagedResult>
{
    public async Task<GetSystemLogsPagedResult> Handle(GetSystemLogsPagedQuery request, CancellationToken ct)
    {
        var (items, total) = await repository.GetPagedAsync(
            request.DateFrom, request.DateTo, request.Name, request.Level,
            request.Page, request.PageSize, ct);
        var dtos = items.Select(x => new SystemLogDto(x.Id, x.CreatedAt, x.Name, x.Level, x.Body)).ToList();
        return new GetSystemLogsPagedResult(dtos, total);
    }
}
