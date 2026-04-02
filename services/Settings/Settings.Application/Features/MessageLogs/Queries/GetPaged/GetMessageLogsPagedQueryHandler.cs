using MediatR;
using Settings.Domain.AggregatesModel.MessageLogAggregate;

namespace Settings.Application.Features.MessageLogs.Queries.GetPaged;

public sealed class GetMessageLogsPagedQueryHandler(IMessageLogRepository repository)
    : IRequestHandler<GetMessageLogsPagedQuery, GetMessageLogsPagedResult>
{
    public async Task<GetMessageLogsPagedResult> Handle(GetMessageLogsPagedQuery request, CancellationToken ct)
    {
        var (items, total) = await repository.GetPagedAsync(
            request.CompanyName,
            request.Receiver,
            request.DateFrom,
            request.DateTo,
            request.Page,
            request.PageSize,
            ct);

        var dtos = items.Select(x => new MessageLogDto(
            x.Id,
            x.SentAt,
            x.Sender,
            x.Receiver,
            x.CompanyName,
            x.Theme,
            x.Body,
            x.LinkUrl,
            x.LinkText)).ToList();

        return new GetMessageLogsPagedResult(dtos, total);
    }
}
