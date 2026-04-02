using MediatR;
using Settings.Domain.AggregatesModel.MessageLogAggregate;

namespace Settings.Application.Features.MessageLogs.Commands.Add;

public sealed class AddMessageLogCommandHandler(IMessageLogRepository repository)
    : IRequestHandler<AddMessageLogCommand, long>
{
    public async Task<long> Handle(AddMessageLogCommand request, CancellationToken ct)
    {
        var dto = request.Dto;
        var entity = new MessageLog
        {
            SentAt = DateTime.UtcNow,
            Sender = dto.Sender,
            Receiver = dto.Receiver,
            CompanyName = dto.CompanyName,
            Theme = dto.Theme,
            Body = dto.Body,
            LinkUrl = dto.LinkUrl,
            LinkText = dto.LinkText
        };
        await repository.AddAsync(entity, ct);
        return entity.Id;
    }
}
